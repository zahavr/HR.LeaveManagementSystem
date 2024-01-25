﻿using HR.LeaveManagementSystem.UI.Components.LeaveTypes;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    public List<LeaveTypeViewModel> LeaveTypes { get; set; } = new ();

    public string Message { get; set; } = string.Empty;

    private LeaveTypeModal Modal { get; set; } = new();

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create");
    }
    
    protected void EditLeaveType(int id)
    {
        Modal.Open();
        // NavigationManager.NavigateTo("/leavetypes/edit");
    }
    
    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo("/leavetypes/details");
    }

    protected async Task DeleteLeaveType(int id)
    {
        var response = await LeaveTypeService.DeleteLeaveType(id);
        
        if(response.Success)
            StateHasChanged();
        else
            Message = response.Message;
    }

    protected void AllocateLeaveType(int id)
    {
        throw new NotImplementedException();
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }
}