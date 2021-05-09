using Company.Domain.Refs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations
{
    public class CountryRefConfiguration: IEntityTypeConfiguration<CountryRef>
    {
        public void Configure(EntityTypeBuilder<CountryRef> builder)
        {
            builder.ToTable("CountryRef", "LK");
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