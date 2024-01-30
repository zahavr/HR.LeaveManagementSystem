using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using HR.LeaveManagementSystem.UI.Constants;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagementSystem.UI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public ApiAuthenticationStateProvider(
        ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity());
        bool isTokenPresent = await _localStorageService.ContainKeyAsync(LocalStorageItems.Token);
        if (isTokenPresent == false)
            return new AuthenticationState(user);

        string savedToken = await _localStorageService.GetItemAsync<string>(LocalStorageItems.Token);
        JwtSecurityToken tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

        if (tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorageService.RemoveItemAsync(LocalStorageItems.Token);
            return new AuthenticationState(user);
        }

        List<Claim> claims = await GetClaims();
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        List<Claim> claims = await GetClaims();
        ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorageService.RemoveItemAsync(LocalStorageItems.Token);
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<List<Claim>> GetClaims()
    {
        string savedToken = await _localStorageService.GetItemAsync<string>(LocalStorageItems.Token);
        JwtSecurityToken tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        List<Claim> claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}