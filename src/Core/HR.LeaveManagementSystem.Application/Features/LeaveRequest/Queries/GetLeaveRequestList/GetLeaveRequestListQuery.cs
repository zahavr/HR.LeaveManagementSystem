using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery(bool IsLoggedUser) : IRequest<List<LeaveRequestListDto>>;