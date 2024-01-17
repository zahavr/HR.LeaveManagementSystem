using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }
    
    public async Task<Unit> Handle(
        DeleteLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        await _leaveRequestRepository.DeleteAsync(leaveRequest);
        return Unit.Value;
    }
}