using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using HR.LeaveManagementSystem.Application.Models.Identity;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;

public class LeaveRequestListDto
{
    public int Id { get; set; }
    
    public bool? Approved { get; set; }
    
    public bool? Cancelled { get; set; }

    public string RequestEmployeeId { get; set; } = string.Empty;
    
    public DateTime DateRequested { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Employee Employee { get; set; } = new();
    
    public LeaveTypeDto LeaveType { get; set; } = new();
}