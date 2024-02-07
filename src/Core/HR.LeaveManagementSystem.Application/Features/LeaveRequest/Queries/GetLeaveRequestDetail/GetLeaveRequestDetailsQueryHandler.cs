using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using HR.LeaveManagementSystem.Application.Models.Identity;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IUserService userService,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        Employee employee = await _userService.GetEmployee(leaveRequest.RequestEmployeeId);
        var leaveRequestDetails = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
        leaveRequestDetails.Employee = employee;
        return leaveRequestDetails;
    }
}