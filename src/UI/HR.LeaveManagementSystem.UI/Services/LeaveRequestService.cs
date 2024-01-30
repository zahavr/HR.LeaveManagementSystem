using Blazored.LocalStorage;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(
        IClient client,
        ILocalStorageService localStorageService)
        : base(client, localStorageService)
    {
    }
}