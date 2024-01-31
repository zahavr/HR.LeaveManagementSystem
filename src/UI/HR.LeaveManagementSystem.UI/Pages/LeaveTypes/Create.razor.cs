using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        [Inject]
        ILeaveTypeService Client { get; set; }

        public string Message { get; private set; }

        LeaveTypeViewModel leaveType = new LeaveTypeViewModel();
        
        async Task CreateLeaveType()
        {
            var response = await Client.CreateLeaveTypes(leaveType);
            if(response.Success)
            {
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}