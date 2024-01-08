using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface ILeaveRequestRepository<T> : IGenericRepository<LeaveType> where T : class
{
    
}