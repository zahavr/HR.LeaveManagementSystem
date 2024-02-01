using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveRequests
{
    public partial class Index
    {
        [Inject] 
        ILeaveRequestService LeaveRequestService { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }
        
        public AdminLeaveRequestViewViewModel Model { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            Model = await LeaveRequestService.GetAdminLeaveRequestList();
        }
    }
}