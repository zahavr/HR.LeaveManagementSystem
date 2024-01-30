using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Details
    {
        [Inject]
        ILeaveTypeService _client { get; set; }

        [Parameter]
        public int id { get; set; }

        LeaveTypeViewModel leaveType = new LeaveTypeViewModel();

        protected override async Task OnParametersSetAsync()
        {
            leaveType = await _client.GetLeaveTypeDetails(id);
        }
    }
}