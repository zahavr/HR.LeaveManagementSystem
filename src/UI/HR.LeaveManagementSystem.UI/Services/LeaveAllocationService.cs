using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient client) : base(client)
    {
    }
}