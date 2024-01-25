using HR.LeaveManagementSystem.Application.Models.Identity;

namespace HR.LeaveManagementSystem.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);

    Task<RegistrationResponse> Register(RegistrationRequest request);
}