using FluentValidation;
using HR.LeaveManagementSystem.Application.Models.Identity;

namespace HR.LeaveManagementSystem.Application.Validators;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");
        
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");
        
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("{PropertyName} should be email");
        
        RuleFor(p => p.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MinimumLength(6).WithMessage("{PropertyName} must be greater than 6");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MinimumLength(6).WithMessage("{PropertyName} must be greater than 6");
    }
}