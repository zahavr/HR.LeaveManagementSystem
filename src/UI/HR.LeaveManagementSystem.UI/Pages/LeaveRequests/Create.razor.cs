using Blazored.Toast.Services;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using HR.LeaveManagementSystem.UI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] 
        ILeaveTypeService LeaveTypeService { get; set; }
        
        [Inject] 
        ILeaveRequestService LeaveRequestService { get; set; }
        
        [Inject] 
        IAuthenticationService AuthenticationService { get; set; }
        
        [Inject]
        private AuthenticationStateProvider StateProvider { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }
        
        LeaveRequestViewModel LeaveRequest { get; set; } = new();
        
        List<LeaveTypeViewModel> LeaveTypes { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await ((ApiAuthenticationStateProvider)StateProvider).GetAuthenticationStateAsync();
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
            if (await AuthenticationService.IsEmployee())
            {
                NavigationManager.NavigateTo("/leaverequests/employeeindex");
            }
            if (await AuthenticationService.IsAdmin())
            {
                NavigationManager.NavigateTo("/leaverequests/");
            }
        }
    }
}