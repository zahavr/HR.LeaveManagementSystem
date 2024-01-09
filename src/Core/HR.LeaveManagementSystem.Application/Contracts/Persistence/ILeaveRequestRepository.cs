using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);

    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails();
    
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestWithDetails(string employeeId);
}