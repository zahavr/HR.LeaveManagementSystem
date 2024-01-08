using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface ILeaveTypeRepository<T> : IGenericRepository<LeaveType> where T : class
{
    
}