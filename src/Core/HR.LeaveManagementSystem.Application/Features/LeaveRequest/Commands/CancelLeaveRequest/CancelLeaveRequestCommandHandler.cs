using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;
using HR.LeaveManagementSystem.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommand> _logger;
    private readonly IMapper _mapper;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CancelLeaveRequestCommand> logger,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(
        CancelLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        leaveRequest.Cancelled = true;

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Subject = LeaveRequestMessages.LeaveRequestCancelled(leaveRequest.StartDate, leaveRequest.EndDate),
                Body = "Leave Request Cancelled"
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