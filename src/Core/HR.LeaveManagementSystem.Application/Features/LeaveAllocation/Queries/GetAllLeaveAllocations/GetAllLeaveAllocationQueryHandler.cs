using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetAllLeaveAllocationQueryHandler : IRequestHandler<GetAllLeaveAllocationQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetAllLeaveAllocationQueryHandler> _logger;

    public GetAllLeaveAllocationQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<GetAllLeaveAllocationQueryHandler> logger,
        IMapper mapper)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }
    
    public async Task<List<LeaveAllocationDto>> Handle(
        GetAllLeaveAllocationQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.LeaveAllocation> leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        List<LeaveAllocationDto> data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        _logger.LogInformation("Leave allocations were retrieved successfully");
        return data;
    }
}