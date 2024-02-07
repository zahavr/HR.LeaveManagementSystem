using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveRequests
{
    public partial class EmployeeIndex
    {
        [Inject] 
        ILeaveRequestService leaveRequestService { get; set; }
        
        [Inject] 
        IJSRuntime JsRuntime { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }
        
        public EmployeeLeaveRequestViewModel Model { get; set; } = new();
        
        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Model = await leaveRequestService.GetUserLeaveRequests();
        }

        async Task CancelRequestAsync(int id)
        {
            var confirm = await JsRuntime.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
            if (confirm)
            {
                var response = await leaveRequestService.CancelLeaveRequest(id);
                if (response.Success)
                {
                    Model = await leaveRequestService.GetUserLeaveRequests();
                    StateHasChanged();
                }
                else
                {
                    Message = response.Message;
                }
            }
        }
    }
}