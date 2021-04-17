using Company.Domain.Refs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations
{
    public class SectorRefConfiguration: IEntityTypeConfiguration<SectorRef>
    {
        public void Configure(EntityTypeBuilder<SectorRef> builder)
        {
            builder.ToTable("SectorRef", "LK");
            builder.HasKey(s => s.RefId);

            builder.Property(s => s.RefId)
                .HasMaxLength(24)
                .IsRequired();
            
            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}