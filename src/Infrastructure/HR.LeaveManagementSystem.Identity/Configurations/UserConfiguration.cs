using HR.LeaveManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagementSystem.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "ac47c53b-8f21-4687-897b-f9760455fd8c",
                UserName = "admin@localhost.com",
                NormalizedUserName = "admin@localhost.com".ToUpperInvariant(),
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com".ToUpperInvariant(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                FirstName = "Admin",
                LastName = "System"
            },
            new ApplicationUser
            {
                Id = "0cac4c0d-c9e5-428d-9fbf-30d13de55a8e",
                UserName = "user@localhost.com",
                NormalizedUserName = "user@localhost.com".ToUpperInvariant(),
                Email = "user@localhost.com",
                NormalizedEmail = "user@localhost.com".ToUpperInvariant(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                FirstName = "User",
                LastName = "System"
            });
    }
}