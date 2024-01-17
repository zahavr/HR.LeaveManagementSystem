using FluentValidation;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
    }
}