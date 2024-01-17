using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand(
    int Id,
    int LeaveTypeId,
    bool Cancelled,
    DateTime StartDate,
    DateTime EndDate,
    string RequestComments) : BaseLeaveRequest(LeaveTypeId, StartDate, EndDate), IRequest<Unit>;
