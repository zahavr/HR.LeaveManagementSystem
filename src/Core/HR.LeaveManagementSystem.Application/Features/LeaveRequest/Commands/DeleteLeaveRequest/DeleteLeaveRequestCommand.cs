using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public record DeleteLeaveRequestCommand(int Id) : IRequest<Unit>;