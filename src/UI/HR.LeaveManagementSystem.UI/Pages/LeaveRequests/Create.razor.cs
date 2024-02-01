using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] 
        ILeaveTypeService LeaveTypeService { get; set; }
        
        [Inject] 
        ILeaveRequestService LeaveRequestService { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }
        
        LeaveRequestViewModel LeaveRequest { get; set; } = new();
        
        List<LeaveTypeViewModel> LeaveTypes { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            // Perform form submission here
            await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}