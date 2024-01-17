using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;

public class LeaveRequestDetailsDto
{
    public bool Cancelled { get; set; }

    public bool? Approved { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime RequestedDate { get; set; }

    public DateTime? DateActioned { get; set; }

    public string RequestEmployeeId { get; set; } = string.Empty;

    public string RequestComments { get; set; } = string.Empty;

    public int LeaveTypeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; } = new();
}