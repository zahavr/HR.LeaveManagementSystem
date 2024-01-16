using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailsQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveAllocationDetailsDto> Handle(
        GetLeaveAllocationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveAllocation leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        
        if (leaveAllocation is null)
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);
        
        LeaveAllocationDetailsDto data = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

        return data;
    }
}