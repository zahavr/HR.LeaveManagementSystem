using HR.LeaveManagementSystem.Application.Models.Identity;

namespace HR.LeaveManagementSystem.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();

    Task<Employee> GetEmployee(string userId);

    public string? UserId { get; }
}