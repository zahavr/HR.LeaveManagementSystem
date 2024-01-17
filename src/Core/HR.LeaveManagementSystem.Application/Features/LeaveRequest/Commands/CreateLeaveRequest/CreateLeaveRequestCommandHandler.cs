using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;
using HR.LeaveManagementSystem.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CreateLeaveRequestCommandHandler> logger,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(
        CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            var emailMessage = new EmailMessage
            {
                To = string.Empty,
                Subject = LeaveRequestMessages.LeaveRequestSubmitted(request.StartDate, request.EndDate),
                Body = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(emailMessage);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        return leaveRequest.Id;
    }
}