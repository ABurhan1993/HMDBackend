using Microsoft.EntityFrameworkCore;
using CrmBackend.Domain.Entities;

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
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<WorkScope> WorkScopes { get; set; }
    public DbSet<InquiryWorkscope> InquiryWorkscopes { get; set; }
    public DbSet<WorkscopeQuotationDetail> WorkscopeQuotationDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.CustomerContact)
            .IsUnique();

        // لا يوجد HasData هنا، كل الـ Seeding يتم عبر SeedData.InitializeAsync()
    }
}
