using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Inject]
        ILeaveTypeService _client { get; set; }

        [Inject]
        NavigationManager _navManager { get; set; }

        [Parameter]
        public int id { get; set; }
        public string Message { get; private set; }

        LeaveTypeViewModel leaveType = new LeaveTypeViewModel();

        protected async override Task OnParametersSetAsync()
        {
            leaveType = await _client.GetLeaveTypeDetails(id);
        }

        async Task EditLeaveType()
        {
            var response = await _client.UpdateLeaveType(leaveType);
            if (response.Success)
            {
                _navManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}