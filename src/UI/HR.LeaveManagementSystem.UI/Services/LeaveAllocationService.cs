using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(
        IClient client,
        ILocalStorageService localStorageService)
        : base(client, localStorageService)
    {
    }

    public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveAllocationCommand createLeaveAllocationCommand = new() { LeaveTypeId = leaveTypeId };

            await _client.LeaveAllocationsPOSTAsync(createLeaveAllocationCommand);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiException<Guid>(e);
        }
    }
}