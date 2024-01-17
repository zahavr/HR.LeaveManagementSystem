namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;

public record BaseLeaveRequest(
    int LeaveTypeId,
    DateTime StartDate,
    DateTime EndDate);