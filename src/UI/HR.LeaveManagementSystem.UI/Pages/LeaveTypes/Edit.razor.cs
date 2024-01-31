using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Inject]
        ILeaveTypeService Client { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }
        
        public string Message { get; private set; }
        

        LeaveTypeViewModel LeaveTypeViewModel = new LeaveTypeViewModel();

        protected async override Task OnParametersSetAsync()
        {
            LeaveTypeViewModel = await Client.GetLeaveTypeDetails(Id);
        }

        async Task EditLeaveType()
        {
            var response = await Client.UpdateLeaveType(LeaveTypeViewModel);
            if (response.Success)
            {
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}