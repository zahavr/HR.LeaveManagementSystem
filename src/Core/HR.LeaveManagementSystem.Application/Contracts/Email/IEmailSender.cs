using HR.LeaveManagementSystem.Application.Models.Email;

namespace HR.LeaveManagementSystem.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}