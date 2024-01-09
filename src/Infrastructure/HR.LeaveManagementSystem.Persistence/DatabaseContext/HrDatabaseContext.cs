using HR.LeaveManagementSystem.Domain;
using HR.LeaveManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR.LeaveManagementSystem.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
    public DbSet<LeaveType> LeaveTypes { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    
    public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options) : base(options)
    {
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedOrAddedEntities = base.ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        
        foreach (EntityEntry<BaseEntity> entity in modifiedOrAddedEntities)
        {
            entity.Entity.DateCreated = DateTime.UtcNow;
            
            if (entity.State == EntityState.Modified)
            {
                entity.Entity.DateModified = DateTime.UtcNow;
            }    
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}