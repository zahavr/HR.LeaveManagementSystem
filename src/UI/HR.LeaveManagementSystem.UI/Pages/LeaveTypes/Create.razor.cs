using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject]
        NavigationManager _navManager { get; set; }
        [Inject]
        ILeaveTypeService _client { get; set; }

        public string Message { get; private set; }

        LeaveTypeViewModel leaveType = new LeaveTypeViewModel();
        async Task CreateLeaveType()
        {
            var response = await _client.CreateLeaveTypes(leaveType);
            if(response.Success)
            {
                _navManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}