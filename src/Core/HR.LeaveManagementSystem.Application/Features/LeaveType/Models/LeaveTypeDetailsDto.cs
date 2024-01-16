namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Models;

public class LeaveTypeDetailsDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }

    public DateTime? DateCreated { get; set; }
    
    public DateTime? DateModified { get; set; }
}