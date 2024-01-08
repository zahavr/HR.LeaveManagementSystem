namespace HR.LeaveManagementSystem.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    
    Task<T> GetById(int id);
    
    Task<T> CreateAsync(T entity);
    
    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(T entity);
}