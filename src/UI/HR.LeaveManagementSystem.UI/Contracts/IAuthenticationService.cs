using HR.LeaveManagementSystem.UI.Models;

namespace HR.LeaveManagementSystem.UI.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);

    Task<bool> RegisterAsync(RegisterViewModel registerViewModel);

    Task Logout();
}