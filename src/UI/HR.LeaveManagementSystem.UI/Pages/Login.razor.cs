using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagementSystem.UI.Pages
{
    public partial class Login
    {
        public LoginViewModel Model { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        public string Message { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        public Login()
        {

        }

        protected override void OnInitialized()
        {
            Model = new LoginViewModel();
        }

        protected async Task HandleLogin()
        {
            if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
            {
                NavigationManager.NavigateTo("/");
            }
            Message = "Username/password combination unknown";
        }
    }
}