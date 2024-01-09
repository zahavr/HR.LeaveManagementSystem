using HR.LeaveManagementSystem.Domain.Common;

namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    
    Task<T> GetByIdAsync(int id);
    
    Task<T> CreateAsync(T entity);
    
    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}