using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Models.Identity;
using HR.LeaveManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagementSystem.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(e => new Employee
        {
            Id = e.Id,
            Email = e.Email,
            FirstName = e.FirstName,
            LastName = e.LastName
        }).ToList();
    }

    public async Task<Employee> GetEmployee(string userId)
    {
        ApplicationUser? employee = await _userManager.FindByIdAsync(userId);
        if (employee is null)
            throw new NotFoundException(nameof(Employee), userId);

        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }
}