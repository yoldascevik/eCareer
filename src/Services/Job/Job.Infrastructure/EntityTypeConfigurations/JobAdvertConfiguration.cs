using Job.Domain.Constants;
using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobAdvertConfiguration : IEntityTypeConfiguration<JobAdvert>
    {
        public void Configure(EntityTypeBuilder<JobAdvert> builder)
        {
            builder.ToTable(nameof(JobAdvert));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CompanyId)
                .IsRequired();

            builder.Property(p => p.LanguageId)
                .HasMaxLength(24)
                .IsRequired();

            builder.Property(p => p.SectorId)
                .HasMaxLength(24)
                .IsRequired();

            builder.Property(p => p.JobPositionId)
                .HasMaxLength(24)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder.Property(p => p.Description)
                .IsUnicode()
                .IsRequired();

            builder.Property(p => p.MinExperienceYear)
                .HasDefaultValue(0);

            builder.Property(p => p.Gender)
                .HasColumnType("char")
                .HasDefaultValue(GenderType.Unspecified)
                .HasConversion(p => (char) p, p => (GenderType) (int) p);

            builder.Property(p => p.IsPublished)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasMany(p => p.Applications)
                .WithOne(p => p.JobAdvert)
                .HasForeignKey(p => p.JobAdvertId);

            builder.HasMany(p => p.Locations)
                .WithOne(p => p.JobAdvert)
                .HasForeignKey(p => p.JobAdvertId)
                .IsRequired();

            builder.HasMany(p => p.WorkTypes)
                .WithOne(p => p.JobAdvert)
                .HasForeignKey(p => p.JobAdvertId)
                .IsRequired();

            builder.HasMany(p => p.EducationLevels)
                .WithOne(p => p.JobAdvert)
                .HasForeignKey(p => p.JobAdvertId)
                .IsRequired();

            builder.HasMany(p => p.ViewingHistories)
                .WithOne(p => p.JobAdvert)
                .HasForeignKey(p => p.JobAdvertId);

            builder.HasIndex(p => p.Title);
            builder.HasIndex(p => p.SectorId);
            builder.HasIndex(p => p.CompanyId);
            builder.HasIndex(p => p.LanguageId);
            builder.HasIndex(p => p.JobPositionId);
            builder.HasIndex(p => new {p.CompanyId, p.IsDeleted});
            builder.HasIndex(p => new {p.CompanyId, p.IsPublished, p.IsDeleted});
        }
    }
}