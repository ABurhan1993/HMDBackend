using CrmBackend.Domain.Constants;
using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Seeding;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        var passwordHasher = new PasswordHasher<User>();

        // ✅ Create Roles if not exist
        var rolesToAdd = new List<string> { RoleConstants.Admin, RoleConstants.User, RoleConstants.Designer };

        foreach (var roleName in rolesToAdd)
        {
            if (!await context.Roles.AnyAsync(r => r.Name == roleName))
            {
                context.Roles.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleName
                });
            }
        }

        // ✅ Create Branch if not exist
        var branch = await context.Branches.FirstOrDefaultAsync(b => b.Name == "Test");
        if (branch == null)
        {
            branch = new Branch
            {
                Name = "Test",
                IsActive = true,
                IsDeleted = false
            };
            context.Branches.Add(branch);
        }

        await context.SaveChangesAsync(); // Save roles and branch

        // ✅ Get Admin Role Id
        var adminRole = await context.Roles.FirstAsync(r => r.Name == RoleConstants.Admin);

        // ✅ Create Admin user if not exist
        var adminEmail = "admin@crm.com";
        if (!await context.Users.AnyAsync(u => u.Email == adminEmail))
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Super Admin",
                Email = adminEmail,
                RoleId = adminRole.Id,
                BranchId = branch.Id
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");
            context.Users.Add(admin);
        }

        await context.SaveChangesAsync();
    }
}
