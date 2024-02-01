using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Contracts;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewViewModel> GetAdminLeaveRequestList();
    
    Task<EmployeeLeaveRequestViewModel> GetUserLeaveRequests();
    
    Task<Response<Guid>> CreateLeaveRequest(LeaveRequestViewModel leaveRequest);
    
    Task<LeaveRequestViewModel> GetLeaveRequest(int id);
    
    Task DeleteLeaveRequest(int id);
    
    Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved);
    
    Task<Response<Guid>> CancelLeaveRequest(int id);
}