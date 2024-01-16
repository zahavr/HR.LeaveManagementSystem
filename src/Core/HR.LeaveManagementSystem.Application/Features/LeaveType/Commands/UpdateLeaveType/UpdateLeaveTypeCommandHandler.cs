using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveTypeCommandHandler> logger,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}",
                nameof(LeaveType), validationResult);
            throw new BadRequestException("Invalid leave type", validationResult);
        }
        
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);
        
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}