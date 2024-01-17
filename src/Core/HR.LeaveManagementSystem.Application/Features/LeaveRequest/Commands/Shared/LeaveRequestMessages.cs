namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.Shared;

internal static class LeaveRequestMessages
{
    public static string LeaveRequestUpdated(DateTime startDate, DateTime endDate)
    {
        return $"Your leave request for {startDate:D} to {endDate:D} has been updated successfully.";
    }
    
    public static string LeaveRequestSubmitted(DateTime startDate, DateTime endDate)
    {
        return $"Your leave request for {startDate:D} to {endDate:D} has been submitted successfully.";
    }

    public static string LeaveRequestCancelled(DateTime startDate, DateTime endDate)
    {
        return $"Your leave request for {startDate:D} to {endDate:D} has been cancelled successfully.";
    }
    
    public static string LeaveRequestApprovalStatusUpdated(DateTime startDate, DateTime endDate)
    {
        return $"The approval status for your leave request for {startDate:D} to {endDate:D} has been updated successfully.";
    }
}