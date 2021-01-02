using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.ToTable(nameof(JobApplication));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.JobAdvertId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CvId).IsRequired();
            builder.Property(p => p.CoverLetter).IsUnicode().HasMaxLength(250);
            builder.Property(p => p.Channel).HasMaxLength(250);
            builder.Property(p => p.Referance).HasMaxLength(250);
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasIndex(p => p.JobAdvertId);
            builder.HasIndex(p => new {p.JobAdvertId, p.IsDeleted});
            builder.HasIndex(p => new {p.UserId, p.IsDeleted});
        }
    }
}