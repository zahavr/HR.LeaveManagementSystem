using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeViewModel>> GetLeaveTypes();

    Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id);

    Task<Response<Guid>> CreateLeaveTypes(LeaveTypeViewModel leaveTypeViewModel);

    Task<Response<Guid>> UpdateLeaveType(LeaveTypeViewModel leaveTypeViewModel);

    Task<Response<Guid>> DeleteLeaveType(int id);
}