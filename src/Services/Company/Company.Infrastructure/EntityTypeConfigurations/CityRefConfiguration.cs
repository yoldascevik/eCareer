using Company.Domain.Refs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations;

public class CityRefConfiguration: IEntityTypeConfiguration<CityRef>
{
    public void Configure(EntityTypeBuilder<CityRef> builder)
    {
        builder.ToTable("CityRef", "LK");
        builder.HasKey(s => s.RefId);

        builder.Property(s => s.RefId)
            .HasMaxLength(24)
            .IsRequired();
            
        builder.Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}