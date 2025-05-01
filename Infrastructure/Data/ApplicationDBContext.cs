using Microsoft.EntityFrameworkCore;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Common;

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
    public DbSet<InquiryTask> InquiryTasks { get; set; }
    public DbSet<TaskFile> TaskFiles { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Design> Designs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.CustomerContact)
            .IsUnique();

        // لا يوجد HasData هنا، كل الـ Seeding يتم عبر SeedData.InitializeAsync()

        // InquiryTask -> Inquiry (Required)
        modelBuilder.Entity<InquiryTask>()
            .HasOne(t => t.Inquiry)
            .WithMany(i => i.InquiryTasks)
            .HasForeignKey(t => t.InquiryId)
            .OnDelete(DeleteBehavior.Cascade);

        // InquiryTask -> InquiryWorkscope (Optional)
        modelBuilder.Entity<InquiryTask>()
            .HasOne(t => t.InquiryWorkscope)
            .WithMany()
            .HasForeignKey(t => t.InquiryWorkscopeId)
            .OnDelete(DeleteBehavior.SetNull);

        // InquiryTask -> AssignedToUser
        modelBuilder.Entity<InquiryTask>()
            .HasOne(t => t.AssignedToUser)
            .WithMany()
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // InquiryTask -> ApprovedByUser
        modelBuilder.Entity<InquiryTask>()
            .HasOne(t => t.ApprovedByUser)
            .WithMany()
            .HasForeignKey(t => t.ApprovedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // TaskFile -> InquiryTask
        modelBuilder.Entity<TaskFile>()
            .HasOne(f => f.InquiryTask)
            .WithMany(t => t.Files)
            .HasForeignKey(f => f.InquiryTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Measurement -> InquiryTask
        modelBuilder.Entity<Measurement>()
            .HasOne(m => m.InquiryTask)
            .WithOne()
            .HasForeignKey<Measurement>(m => m.InquiryTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Design -> InquiryTask
        modelBuilder.Entity<Design>()
            .HasOne(d => d.InquiryTask)
            .WithOne()
            .HasForeignKey<Design>(d => d.InquiryTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
