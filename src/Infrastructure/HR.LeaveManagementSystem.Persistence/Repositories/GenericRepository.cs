using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Domain.Common;
using HR.LeaveManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagementSystem.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T  : BaseEntity
{
    protected readonly HrDatabaseContext _context;

    public GenericRepository(HrDatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id); 
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}