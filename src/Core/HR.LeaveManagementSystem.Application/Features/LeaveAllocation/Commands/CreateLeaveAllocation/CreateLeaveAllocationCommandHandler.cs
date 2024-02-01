using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Models.Identity;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IUserService userService
        )
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(
        CreateLeaveAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationValidator(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

        Domain.LeaveType? leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        if (leaveType is null)
            throw new NotFoundException(nameof(leaveType), request.LeaveTypeId);

        List<Employee> employees = await _userService.GetEmployees();
        int period = DateTime.Now.Year;
        var allocations = new List<Domain.LeaveAllocation>();
        foreach (Employee employee in employees)
        {
            bool allocationExists =
                await _leaveAllocationRepository.AllocationExists(employee.Id, leaveType.Id, period);
            if (allocationExists)
                continue;
            
            allocations.Add(new Domain.LeaveAllocation
            {
                EmployeeId = employee.Id,
                Period = period,
                NumberOfDays = leaveType.DefaultDays,
                LeaveTypeId = leaveType.Id,
            });
        }

        if(allocations.Any())
            await _leaveAllocationRepository.AddAllocations(allocations);
        
        return Unit.Value;
    }
}