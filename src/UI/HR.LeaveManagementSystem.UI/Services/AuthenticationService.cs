using AutoMapper;
using HR.LeaveManagementSystem.UI.Constants;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models;
using HR.LeaveManagementSystem.UI.Providers;
using HR.LeaveManagementSystem.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagementSystem.UI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _stateProvider;
    private readonly IMapper _mapper;

    public AuthenticationService(
        IClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider stateProvider,
        IMapper mapper)
        : base(client, localStorageService)
    {
        _stateProvider = stateProvider;
        _mapper = mapper;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authRequest = new AuthRequest
            {
                Email = email,
                Password = password
            };
            AuthResponse response = await _client.LoginAsync(authRequest);
            if (!string.IsNullOrEmpty(response.Token))
            {
                await _localStorageService.SetItemAsync(LocalStorageItems.Token, response.Token);
                await ((ApiAuthenticationStateProvider)_stateProvider).LoggedIn();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
    {
        RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registerViewModel);
        var response = await _client.RegisterAsync(registrationRequest);

        if (string.IsNullOrEmpty(response.UserId))
            return false;
        
        return true;
    }

    public async Task Logout()
    {
        await ((ApiAuthenticationStateProvider)_stateProvider).LoggedOut();
    }

    public async Task<bool> IsEmployee()
    {
        return await ((ApiAuthenticationStateProvider)_stateProvider).IsEmployee();
    }
    
    public async Task<bool> IsAdmin()
    {
        return await ((ApiAuthenticationStateProvider)_stateProvider).IsAdmin();
    }
}