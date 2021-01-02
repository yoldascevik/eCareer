using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobLocationConfiguration : IEntityTypeConfiguration<JobLocation>
    {
        public void Configure(EntityTypeBuilder<JobLocation> builder)
        {
            builder.ToTable(nameof(JobLocation));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CountryId).HasMaxLength(24);
            builder.Property(p => p.CityId).HasMaxLength(24);
            builder.Property(p => p.DistrictId).HasMaxLength(24);
            
            builder.HasIndex(p => p.CountryId);
            builder.HasIndex(p => p.CityId);
            builder.HasIndex(p => p.DistrictId);
        }
    }
}