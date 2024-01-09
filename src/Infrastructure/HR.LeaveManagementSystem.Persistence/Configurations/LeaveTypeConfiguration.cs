using HR.LeaveManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagementSystem.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                DateCreated = DateTime.UtcNow,
                DateModified = null,
                DefaultDays = 10,
                Name = "Vacation"
            });

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        
    }
}