using System.Net;
using System.Net.Http.Headers;
using HR.LeaveManagementSystem.UI.Constants;
using HR.LeaveManagementSystem.UI.Contracts;

namespace HR.LeaveManagementSystem.UI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localStorageService;

    public BaseHttpService(
        IClient client,
        ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }

    protected Response<TGuid> ConvertApiException<TGuid>(ApiException apiException) =>
        apiException.StatusCode switch
        {
            (int)HttpStatusCode.BadRequest => new Response<TGuid>()
            {
                Message = "Invalid data was submitted", ValidationErrors = apiException.Response, Success = false
            },
            (int)HttpStatusCode.NotFound => new Response<TGuid>()
            {
                Message = "The record was not found", ValidationErrors = apiException.Response, Success = false
            },
            _ => new Response<TGuid>()
            {
                Message = "Something went wrong, please try again letter",
                ValidationErrors = apiException.Response,
                Success = false
            }
        };

    protected async Task AddBearerToken()
    {
        if (await _localStorageService.ContainKeyAsync(LocalStorageItems.Token))
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",
                    await _localStorageService.GetItemAsync<string>(LocalStorageItems.Token));
    }
}