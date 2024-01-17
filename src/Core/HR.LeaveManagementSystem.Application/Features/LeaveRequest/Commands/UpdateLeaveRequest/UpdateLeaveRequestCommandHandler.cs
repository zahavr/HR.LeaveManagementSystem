using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;
using HR.LeaveManagementSystem.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(
        UpdateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid leave request", validationResult);

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Body = LeaveRequestMessages.LeaveRequestUpdated(request.StartDate, request.EndDate),
                Subject = "Leave Requested Updated"
            };
            await _emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        return Unit.Value;
    }
}