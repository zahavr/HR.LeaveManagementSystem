using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public record DeleteLeaveAllocationCommand(int Id) : IRequest<Unit>;