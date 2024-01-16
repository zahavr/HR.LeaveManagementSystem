using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveAllocation? leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
        if (leaveAllocation is null)
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

        return Unit.Value;
    }
}