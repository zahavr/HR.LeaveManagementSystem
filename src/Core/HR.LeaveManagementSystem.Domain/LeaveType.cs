using HR.LeaveManagementSystem.Domain.Common;

namespace HR.LeaveManagementSystem.Domain;

public class LeaveType : BaseEntity
{
    public int DefaultDays { get; set; }
    
    public string Name { get; set; } = string.Empty;
}