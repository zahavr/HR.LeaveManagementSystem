using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;

public class LeaveRequestListDto
{
    public bool? Approved { get; set; }

    public string RequestingEmployeeId { get; set; } = string.Empty;
    
    public DateTime DateRequested { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public LeaveTypeDto LeaveType { get; set; } = new();

}