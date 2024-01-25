using System.Net;

namespace HR.LeaveManagementSystem.UI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;

    public BaseHttpService(IClient client)
    {
        _client = client;
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
}