using Microsoft.EntityFrameworkCore;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Constants;

namespace CrmBackend.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RoleClaim> RoleClaims => Set<RoleClaim>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerComment> CustomerComments => Set<CustomerComment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRole = new Role { Id = Guid.NewGuid(), Name = RoleConstants.Admin };
        var userRole = new Role { Id = Guid.NewGuid(), Name = RoleConstants.User };
        var designerRole = new Role { Id = Guid.NewGuid(), Name = RoleConstants.Designer };

        modelBuilder.Entity<Role>().HasData(adminRole, userRole, designerRole);
    }
}
