using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Domain;
using HR.LeaveManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagementSystem.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    { }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Include(lr => lr.LeaveType)
            .SingleOrDefaultAsync(lr => lr.Id == id);
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Include(lr => lr.LeaveType)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails(string employeeId)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Where(lr => lr.RequestEmployeeId == employeeId)
            .Include(lr => lr.LeaveType)
            .ToListAsync();
    }
}