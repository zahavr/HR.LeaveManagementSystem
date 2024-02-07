using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] 
        ILeaveRequestService LeaveRequestService { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }
        
        [Parameter]
        public int Id { get; set; }

        string ClassName = string.Empty;

        private string HeadingText = string.Empty;

        public LeaveRequestViewModel Model { get; private set; } = new LeaveRequestViewModel();

        protected override async Task OnParametersSetAsync()
        {
            Model = await LeaveRequestService.GetLeaveRequest(Id);
        }

        protected override Task OnInitializedAsync()
        {
            if (Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
            else if (Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }

            return Task.CompletedTask;
        }

        async Task ChangeApproval(bool approvalStatus)
        {
            await LeaveRequestService.ApproveLeaveRequest(Id, approvalStatus);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}