using System.Net;
using HR.LeaveManagementSystem.API.Models;
using HR.LeaveManagementSystem.Application.Exceptions;

namespace HR.LeaveManagementSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandlerExceptionAsync(context, ex);
        }
    }

    private async Task HandlerExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        ApiResponse apiResponse = new ApiResponse();

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                apiResponse.ErrorMessage = new CustomErrorMessageDetails()
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Erros = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                apiResponse.ErrorMessage = new CustomErrorMessageDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.NotFound),
                    Detail = notFoundException.InnerException?.Message
                };
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                apiResponse.ErrorMessage = new CustomErrorMessageDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.NotFound),
                    Detail = exception.StackTrace
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(apiResponse, CancellationToken.None);
    }
}