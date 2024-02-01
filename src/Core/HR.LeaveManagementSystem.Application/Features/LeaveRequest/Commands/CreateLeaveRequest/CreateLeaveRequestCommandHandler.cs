using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagementSystem.Application.Contracts.Email;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
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
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUserService _userService;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender,
        IUserService userService,
        IAppLogger<CreateLeaveRequestCommandHandler> logger,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _emailSender = emailSender;
        _userService = userService;
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

        string? employeeId = _userService.UserId;
        if (employeeId is null)
            throw new NotFoundException(nameof(employeeId), "userId");

        Domain.LeaveAllocation? allocations = await _leaveAllocationRepository.GetEmployeeAllocations(employeeId, request.LeaveTypeId);
        if (allocations is null)
        {
            validationResult.Errors.Add(new ValidationFailure(nameof(request.LeaveTypeId),
                "You do not have any allocations for this leave type"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
        if (daysRequested > allocations.NumberOfDays)
        {
            validationResult.Errors.Add(new ValidationFailure(nameof(request.EndDate), "You do not have enough days for this request"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }
        
        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.Now;
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