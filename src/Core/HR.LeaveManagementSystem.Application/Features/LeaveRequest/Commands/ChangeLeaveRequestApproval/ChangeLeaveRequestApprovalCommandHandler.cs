using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;
using HR.LeaveManagementSystem.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;
    private readonly IMapper _mapper;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender,
        IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var message = new EmailMessage
            {
                To = string.Empty,
                Subject = LeaveRequestMessages.LeaveRequestApprovalStatusUpdated(leaveRequest.StartDate,
                    leaveRequest.EndDate),
                Body = "Leave Request Approval Status Updated"
            };
            await _emailSender.SendEmail(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        return Unit.Value;
    }
}