using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Models.Email;
using HR.LeaveManagementSystem.Infrastructure.EmailService;
using HR.LeaveManagementSystem.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagementSystem.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        
        services.AddTransient<IEmailSender, EmailSender>();
        
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));        
        
        return services;
    }
}