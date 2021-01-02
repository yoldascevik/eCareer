using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobTagConfiguration: IEntityTypeConfiguration<JobTag>
    {
        public void Configure(EntityTypeBuilder<JobTag> builder)
        {
            builder.ToTable(nameof(JobTag));
            builder.HasKey(jt => new { jt.JobAdvertId, jt.TagId });
            builder.HasOne(jt => jt.JobAdvert)
                .WithMany(j => j.Tags)
                .HasForeignKey(jt => jt.JobAdvertId);
            builder.HasOne(jt => jt.Tag)
                .WithMany(j => j.Tags)
                .HasForeignKey(jt => jt.TagId);
            
            builder.HasIndex(p => p.TagId);
            builder.HasIndex(p => p.JobAdvertId);
        }
    }
}