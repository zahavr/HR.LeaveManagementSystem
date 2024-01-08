using HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;