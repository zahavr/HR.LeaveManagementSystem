namespace HR.LeaveManagementSystem.UI.Components.LeaveTypes;

public partial class LeaveTypeModal
{
    public Guid Guid = Guid.NewGuid();
    public string ModalDisplay = "none;";
    public string ModalClass = "";
    public bool ShowBackdrop = false;

    public void Open()
    {
        ModalDisplay = "block;";
        ModalClass = "Show";
        ShowBackdrop = true;
        StateHasChanged();
    }

    public void Close()
    {
        ModalDisplay = "none";
        ModalClass = "";
        ShowBackdrop = false;
        StateHasChanged();
    }
}