using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations
{
    public class AddressConfiguration: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(nameof(Address));
            builder.HasKey(o => o.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Details)
                .HasMaxLength(250)
                .IsRequired();
            
            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(t => t.IsPrimary)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(t => t.CountryRef)
                .WithMany()
                .IsRequired();

            builder.HasOne(t => t.CityRef)
                .WithMany()
                .IsRequired();

            builder.HasOne(t => t.DistrictRef)
                .WithMany();
        }
    }
}