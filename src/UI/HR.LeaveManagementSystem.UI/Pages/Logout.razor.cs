﻿using HR.LeaveManagementSystem.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages;

public partial class Logout
{
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await AuthenticationService.Logout();
        NavigationManager.NavigateTo("/");
    }
}