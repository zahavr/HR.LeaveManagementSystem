using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository<T> : IGenericRepository<LeaveAllocation> where T : class
{
    
}