using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string employeeId);

    Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);

    Task AddAllocations(List<LeaveAllocation> allocations);

    Task<LeaveAllocation> GetEmployeeAllocations(string employeeId, int leaveTypeId);
}