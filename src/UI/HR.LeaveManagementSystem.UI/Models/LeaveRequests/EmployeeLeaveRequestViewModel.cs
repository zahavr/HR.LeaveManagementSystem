using HR.LeaveManagementSystem.UI.Models.LeaveAllocations;

namespace HR.LeaveManagementSystem.UI.Models.LeaveRequests
{
    public class EmployeeLeaveRequestViewModel
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; } = new();
        
        public List<LeaveRequestViewModel> LeaveRequests { get; set; } = new();
    }
}
