using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>;