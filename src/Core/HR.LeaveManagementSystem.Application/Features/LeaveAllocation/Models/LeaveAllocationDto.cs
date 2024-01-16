using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;

public class LeaveAllocationDto
{
    public int Id { get; set; }

    public int NumberOfDays { get; set; }
    
    public int Period { get; set; }
    
    public int LeaveTypeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }
}