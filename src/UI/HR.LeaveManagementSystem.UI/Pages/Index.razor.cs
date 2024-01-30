using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagementSystem.UI.Pages;

public partial class Index
{
    [Inject]
    private AuthenticationStateProvider StateProvider { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)StateProvider).GetAuthenticationStateAsync();
    }

    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }

    protected void Logout()
    {
        AuthenticationService.Logout();
    }
}