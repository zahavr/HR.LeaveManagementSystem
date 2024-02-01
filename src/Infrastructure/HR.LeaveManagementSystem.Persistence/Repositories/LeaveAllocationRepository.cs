using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Domain;
using HR.LeaveManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagementSystem.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
        
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Include(la => la.LeaveType)
            .SingleOrDefaultAsync(la => la.Id == id);
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Include(la => la.LeaveType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string employeeId)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Where(la => la.EmployeeId == employeeId)
            .Include(la => la.LeaveType)
            .ToListAsync();
    }

    public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(
            la =>
                la.EmployeeId == employeeId &&
                la.LeaveTypeId == leaveTypeId &&
                la.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetEmployeeAllocations(string employeeId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Include(la => la.LeaveType)
            .SingleOrDefaultAsync(la =>
                la.EmployeeId == employeeId &&
                la.LeaveTypeId == leaveTypeId);
    }
}