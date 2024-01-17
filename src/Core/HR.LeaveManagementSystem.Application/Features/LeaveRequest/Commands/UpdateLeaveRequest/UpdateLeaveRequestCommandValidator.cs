using FluentValidation;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist).WithMessage("{PropertyName} must by present");
    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveAllocation = await _leaveRequestRepository.GetByIdAsync(id);
        return leaveAllocation is not null;
    }
}