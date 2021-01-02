using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobWorkTypeConfiguration: IEntityTypeConfiguration<JobWorkType>
    {
        public void Configure(EntityTypeBuilder<JobWorkType> builder)
        {
            builder.ToTable(nameof(JobWorkType));
            builder.HasIndex(p => p.JobAdvertId);
        }
    }
}