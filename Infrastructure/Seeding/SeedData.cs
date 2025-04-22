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
                FullName = "Super Admin",
                FirstName = "Super",
                LastName = "Admin",
                Email = adminEmail,
                Phone = "0500000000",
                UserImageUrl = null,
                IsNotificationEnabled = true,
                RoleId = adminRole.Id,
                BranchId = branch.Id
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");
            context.Users.Add(admin);
        }

        // ✅ Add missing permissions to Admin role
        var existingClaims = await context.RoleClaims
            .Where(rc => rc.RoleId == adminRole.Id)
            .Select(rc => rc.Value)
            .ToListAsync();

        var allPermissions = PermissionConstants.All;
        var newPermissions = allPermissions.Except(existingClaims);

        foreach (var permission in newPermissions)
        {
            context.RoleClaims.Add(new RoleClaim
            {
                RoleId = adminRole.Id,
                Type = "Permission",
                Value = permission
            });
        }

        // ✅ Seed Workscopes if not exist
        if (!await context.WorkScopes.AnyAsync())
        {
            context.WorkScopes.AddRange(new[]
            {
        new WorkScope { WorkScopeName = "Kitchen Cabinets", WorkScopeDescription = "Workscope for kitchen modules" },
        new WorkScope { WorkScopeName = "Wardrobes", WorkScopeDescription = "Workscope for wardrobes" },
        new WorkScope { WorkScopeName = "TV Unit", WorkScopeDescription = "TV wall units and shelves" },
        new WorkScope { WorkScopeName = "Vanity", WorkScopeDescription = "Bathroom cabinetry" },
        new WorkScope { WorkScopeName = "Other", WorkScopeDescription = "Miscellaneous work" }
    });
        }


        await context.SaveChangesAsync(); // Save everything
    }
}
