using System.Net.Http.Headers;
using HR.LeaveManagementSystem.UI.Constants;
using HR.LeaveManagementSystem.UI.Contracts;

namespace HR.LeaveManagementSystem.UI.Handlers;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _localStorageService.GetItemAsync<string>(LocalStorageItems.Token);
        if (string.IsNullOrEmpty(token) is false)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}