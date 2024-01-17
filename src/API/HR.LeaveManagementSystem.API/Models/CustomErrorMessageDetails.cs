using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagementSystem.API.Models;

public class CustomErrorMessageDetails : ProblemDetails
{
    public IDictionary<string, string[]> Erros { get; set; } = new Dictionary<string, string[]>();
}