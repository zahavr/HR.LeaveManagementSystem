using HR.LeaveManagementSystem.Domain.Common;

namespace HR.LeaveManagementSystem.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }
}