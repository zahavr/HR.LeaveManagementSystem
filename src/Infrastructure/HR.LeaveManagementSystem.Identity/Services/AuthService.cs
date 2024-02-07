using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Models.Identity;
using HR.LeaveManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagementSystem.Identity.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
        IOptions<JwtSettings> jwtSettings,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(nameof(request.Email), request.Email);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            throw new BadRequestException($"Credentials for {request.Email} aren`t valid");

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            foreach (IdentityError error in result.Errors)
            {
                sb.AppendFormat("- {0}\n", error.Description);
            }

            throw new BadRequestException($"{sb}");
        }
        
    }
    
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        List<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
        IEnumerable<Claim> claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}