using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.EntityTypeConfigurations;

public class CompanyConfiguration: IEntityTypeConfiguration<Domain.Entities.Company>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
    {
        builder.ToTable("Company");
        builder.HasKey(o => o.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        // value object mapping
        builder.OwnsOne(m => m.TaxInfo, a =>
        {
            a.Property(t => t.TaxNumber)
                .HasColumnName("TaxNumber")
                .HasMaxLength(50)
                .IsRequired();
            
            a.Property(t => t.TaxOffice)
                .HasColumnName("TaxOffice")
                .HasMaxLength(50)
                .IsRequired();
                
            a.Property(t => t.TaxCountryId)
                .HasColumnName("TaxCountryId")
                .HasMaxLength(24)
                .IsRequired();
        });
            
        builder.HasOne(t => t.Sector)
            .WithMany()
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

        builder.Property(t => t.EmployeesCount)
            .HasColumnType("smallint");
            
        builder.Property(t => t.EstablishedYear)
            .HasColumnType("smallint");

        builder.Property(t => t.IsDeleted)
            .HasDefaultValue(0);
            
        builder.Property(t => t.CreationTime)
            .IsRequired();
            
        builder.HasMany(c => c.Followers)
            .WithOne(e => e.Company);

        builder.HasMany(t => t.Addresses)
            .WithOne()
            .HasForeignKey(x => x.CompanyId);
    }
}