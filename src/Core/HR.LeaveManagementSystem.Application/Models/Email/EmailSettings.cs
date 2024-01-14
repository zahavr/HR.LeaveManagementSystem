namespace HR.LeaveManagementSystem.Application.Models.Email;

public class EmailSettings
{
    public const string SectionName = "EmailSettings"; 
    public string ApiKey { get; set; }

    public string FromAddress { get; set; }

    public string FromName { get; set; }
}