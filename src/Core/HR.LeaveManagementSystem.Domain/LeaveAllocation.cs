using HR.LeaveManagementSystem.Domain.Common;

namespace HR.LeaveManagementSystem.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    
    public int Period { get; set; }
    
    public int LeaveTypeId { get; set; }
    
    public LeaveType? LeaveType { get; set; }
}