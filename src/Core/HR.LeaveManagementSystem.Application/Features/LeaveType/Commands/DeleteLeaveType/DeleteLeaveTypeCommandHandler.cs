using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
 

    public DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveType leaveTypeToDelete = await _leaveTypeRepository.GetById(request.Id);

        if (leaveTypeToDelete is null)
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        
        return Unit.Value;
    }
}