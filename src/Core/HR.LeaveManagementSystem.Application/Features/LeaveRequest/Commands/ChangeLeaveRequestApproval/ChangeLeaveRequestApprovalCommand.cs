using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public record ChangeLeaveRequestApprovalCommand(int Id, bool Approved) : IRequest<Unit>;