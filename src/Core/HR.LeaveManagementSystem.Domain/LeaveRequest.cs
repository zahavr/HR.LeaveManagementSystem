using HR.LeaveManagementSystem.Domain.Common;

namespace HR.LeaveManagementSystem.Domain;

public class LeaveRequest : BaseEntity
{
    public bool Cancelled { get; set; }
    
    public bool? Approved { get; set; }

    public string RequestEmployeeId { get; set; } = string.Empty;

    public string? RequestComments { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public DateTime DateRequested { get; set; }
    
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
}