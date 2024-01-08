using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand(int Id, int DefaultDays, string Name) : IRequest<Unit>;