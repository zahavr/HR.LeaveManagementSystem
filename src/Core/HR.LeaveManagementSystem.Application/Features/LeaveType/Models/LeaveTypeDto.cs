namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Models;

public class LeaveTypeDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }
}