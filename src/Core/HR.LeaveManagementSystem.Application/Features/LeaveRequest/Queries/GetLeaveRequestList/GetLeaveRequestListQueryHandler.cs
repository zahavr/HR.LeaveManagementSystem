using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using HR.LeaveManagementSystem.Application.Models.Identity;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetLeaveRequestListQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IUserService userService,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<List<LeaveRequestListDto>> Handle(
        GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.LeaveRequest> leaveRequests;
        List<LeaveRequestListDto> requests;

        if (request.IsLoggedUser)
        {
            string? userId = _userService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new BadRequestException("User id not provided.");
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            Employee employee = await _userService.GetEmployee(userId);
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (LeaveRequestListDto req in requests)
            {
                req.Employee = employee;
            }

            return requests;
        }

        leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
        foreach (LeaveRequestListDto req in requests)
        {
            req.Employee = await _userService.GetEmployee(req.RequestEmployeeId);
        }

        return requests;
    }
}