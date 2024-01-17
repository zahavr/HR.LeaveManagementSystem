using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
    }
}