using FluentValidation;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType is not null;
    }
}