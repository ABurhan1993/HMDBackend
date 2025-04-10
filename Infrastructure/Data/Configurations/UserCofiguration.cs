using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrmBackend.Domain.Entities;

namespace CrmBackend.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);

        builder.HasOne(x => x.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Branch)
               .WithMany(b => b.Users)
               .HasForeignKey(x => x.BranchId);
    }
}
