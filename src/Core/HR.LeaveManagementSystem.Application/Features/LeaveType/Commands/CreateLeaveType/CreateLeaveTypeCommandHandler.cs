using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

    public CreateLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<CreateLeaveTypeCommandHandler> logger,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(
        CreateLeaveTypeCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}",
                nameof(LeaveType), validationResult);
            throw new BadRequestException("Invalid leave type", validationResult);
        }
        
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        Domain.LeaveType leaveType = await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        return leaveType.Id;
    }
}