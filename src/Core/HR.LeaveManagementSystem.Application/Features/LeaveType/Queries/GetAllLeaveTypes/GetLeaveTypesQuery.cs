using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;