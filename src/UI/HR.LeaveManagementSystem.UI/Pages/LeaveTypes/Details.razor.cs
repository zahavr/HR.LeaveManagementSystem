using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Details
    {
        [Inject]
        ILeaveTypeService Client { get; set; }

        [Parameter]
        public int Id { get; set; }

        LeaveTypeViewModel LeaveTypeViewModel = new LeaveTypeViewModel();

        protected override async Task OnParametersSetAsync()
        {
            LeaveTypeViewModel = await Client.GetLeaveTypeDetails(Id);
        }
    }
}