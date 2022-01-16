using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations;

public class CompanyFollowerConfiguration : IEntityTypeConfiguration<CompanyFollower>
{
    public void Configure(EntityTypeBuilder<CompanyFollower> builder)
    {
        builder.ToTable("CompanyFollower");
            
        builder.HasKey(o => o.Id);
        builder.Property(t => t.CompanyId).IsRequired();
        builder.Property(t => t.UserId).IsRequired();
        builder.Property(t => t.IsDeleted).HasDefaultValue(0);
    }
}