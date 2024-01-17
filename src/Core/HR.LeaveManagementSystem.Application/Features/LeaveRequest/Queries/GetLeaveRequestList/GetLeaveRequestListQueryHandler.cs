using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestListQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<List<LeaveRequestListDto>> Handle(
        GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        List<LeaveRequestListDto> requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        return requests;
    }
}