using Blazored.Toast.Services;
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
        
        [Inject] 
        IToastService ToastService { get; set; }

        LeaveTypeViewModel LeaveType = new();
        
        async Task CreateLeaveType()
        {
            var response = await Client.CreateLeaveTypes(LeaveType);
            if(response.Success)
            {
                ToastService.ShowSuccess("Leave Type created");
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}