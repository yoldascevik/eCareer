using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobViewingHistoryConfiguration: IEntityTypeConfiguration<JobViewingHistory>
    {
        public void Configure(EntityTypeBuilder<JobViewingHistory> builder)
        {
            builder.ToTable(nameof(JobViewingHistory));
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Channel).HasMaxLength(250);
            builder.Property(p => p.Referance).HasMaxLength(250);
            
            builder.HasIndex(p => p.JobAdvertId);
        }
    }
}