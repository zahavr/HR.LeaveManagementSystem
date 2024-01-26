using HR.LeaveManagementSystem.Identity.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagementSystem.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "18726326-54d6-4998-8d16-01dd91eaa7c1",
                Name = Roles.Employee,
                NormalizedName = Roles.Employee.ToUpperInvariant(),
            },
            new IdentityRole
            {
                Id = "df3eca98-b606-4c7c-a4ea-6f639d7766d1",
                Name = Roles.Administrator,
                NormalizedName = Roles.Administrator.ToUpperInvariant(),
            });
    }
}