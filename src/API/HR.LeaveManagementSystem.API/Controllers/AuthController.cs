using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authService.Login(request));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Login(RegistrationRequest request)
    {
        return Ok(await _authService.Register(request));
    }
}