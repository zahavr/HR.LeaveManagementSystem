using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public record CreateLeaveRequestCommand(
    int LeaveTypeId,
    DateTime StartDate,
    DateTime EndDate,
    string RequestComments) : BaseLeaveRequest(LeaveTypeId, StartDate, EndDate), IRequest<int>;