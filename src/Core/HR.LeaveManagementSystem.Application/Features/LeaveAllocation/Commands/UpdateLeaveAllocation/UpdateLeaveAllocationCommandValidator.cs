using FluentValidation;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(l => l.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        
        RuleFor(l => l.Period)
            .GreaterThan(DateTime.UtcNow.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist");

        RuleFor(l => l.Id)
            .NotNull().WithMessage("{PropertyName} must be present.")
            .MustAsync(LeaveAllocationMustExist);
    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);

        return leaveAllocation is not null;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType is not null;
    }
}