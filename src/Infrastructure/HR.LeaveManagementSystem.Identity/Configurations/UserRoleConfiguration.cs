using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagementSystem.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "ac47c53b-8f21-4687-897b-f9760455fd8c",
                RoleId = "df3eca98-b606-4c7c-a4ea-6f639d7766d1"
            },
            new IdentityUserRole<string>
            {
                UserId = "0cac4c0d-c9e5-428d-9fbf-30d13de55a8e",
                RoleId = "18726326-54d6-4998-8d16-01dd91eaa7c1"
            });
    }
}