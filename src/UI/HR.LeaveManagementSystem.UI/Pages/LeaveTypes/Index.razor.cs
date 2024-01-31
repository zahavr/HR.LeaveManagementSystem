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
    

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }
    
    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }
    
    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(int id)
    {
        var response = await LeaveTypeService.DeleteLeaveType(id);

        if (response.Success)
        {
            LeaveTypes.Remove(LeaveTypes.Single(lt => lt.Id == id));
            StateHasChanged();
        }
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