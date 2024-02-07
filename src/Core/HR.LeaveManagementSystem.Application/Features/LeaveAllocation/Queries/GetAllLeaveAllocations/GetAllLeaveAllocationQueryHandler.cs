using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetAllLeaveAllocationQueryHandler : IRequestHandler<GetAllLeaveAllocationQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IUserService _userService;
    private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

    public GetAllLeaveAllocationQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IUserService userService,
        IAppLogger<GetAllLeaveAllocationQueryHandler> logger,
        IMapper mapper)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _userService = userService;
        _logger = logger;
    }
    
    public async Task<List<LeaveAllocationDto>> Handle(
        GetAllLeaveAllocationQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.LeaveAllocation> leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        List<LeaveAllocationDto> data;
        if (request.IsLoggedUser)
        {
            string? userId = _userService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new BadRequestException("User id not found.");

            leaveAllocations = leaveAllocations.Where(la => la.EmployeeId == userId).ToList();
            data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
            return data;
        }
        
        data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        _logger.LogInformation("Leave allocations were retrieved successfully");
        return data;
    }
}