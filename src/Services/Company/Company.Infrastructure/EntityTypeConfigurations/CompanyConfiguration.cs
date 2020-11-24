using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations
{
    public class CompanyConfiguration: IEntityTypeConfiguration<Domain.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Company> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(t => t.SectorId)
                .HasMaxLength(24)
                .IsRequired();
            
            builder.Property(t => t.CountryId)
                .HasMaxLength(24)
                .IsRequired();
            
            builder.Property(t => t.CityId)
                .HasMaxLength(24)
                .IsRequired();

            builder.Property(t => t.DistrictId)
                .HasMaxLength(24);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(t => t.TaxNumber)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(t => t.TaxOffice)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Website)
                .HasMaxLength(50);
            
            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(t => t.Phone)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.MobilePhone)
                .HasMaxLength(50);
            
            builder.Property(t => t.FaxNumber)
                .HasMaxLength(50);
            
            builder.Property(t => t.Address)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.EmployeesCount)
                .HasColumnType("smallint");
            
            builder.Property(t => t.EstablishedYear)
                .HasColumnType("smallint");

            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(0);
            
            builder.Property(t => t.CreationTime)
                .IsRequired();
        }
    }
}