using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand(int Id, int NumberOfDays, int LeaveTypeId, int Period): IRequest<Unit>;