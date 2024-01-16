using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;