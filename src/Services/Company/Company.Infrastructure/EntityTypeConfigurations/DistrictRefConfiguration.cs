using Company.Domain.Refs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations;

public class DistrictRefConfiguration: IEntityTypeConfiguration<DistrictRef>
{
    public void Configure(EntityTypeBuilder<DistrictRef> builder)
    {
        builder.ToTable("DistrictRef", "LK");
        builder.HasKey(s => s.RefId);

        builder.Property(s => s.RefId)
            .HasMaxLength(24)
            .IsRequired();
            
        builder.Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}