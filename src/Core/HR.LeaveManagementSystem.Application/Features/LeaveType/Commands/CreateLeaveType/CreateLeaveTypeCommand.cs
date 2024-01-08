using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.CreateLeaveType;

public record CreateLeaveTypeCommand(int DefaultDays, string Name) : IRequest<int>;