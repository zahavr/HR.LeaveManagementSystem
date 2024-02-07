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
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommand> _logger;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender,
        IAppLogger<CancelLeaveRequestCommand> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _emailSender = emailSender;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(
        CancelLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        leaveRequest.Cancelled = true;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        if (leaveRequest.Approved == true)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            Domain.LeaveAllocation? allocation =
                await _leaveAllocationRepository.GetEmployeeAllocations(leaveRequest.RequestEmployeeId,
                    leaveRequest.LeaveTypeId);
            if (allocation is null)
                throw new NotFoundException(nameof(allocation), leaveRequest.RequestEmployeeId);
            allocation.NumberOfDays += daysRequested;
            await _leaveAllocationRepository.UpdateAsync(allocation);   
        }
        
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