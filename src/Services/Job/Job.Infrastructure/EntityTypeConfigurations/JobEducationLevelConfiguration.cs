using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class JobEducationLevelConfiguration: IEntityTypeConfiguration<JobEducationLevel>
    {
        public void Configure(EntityTypeBuilder<JobEducationLevel> builder)
        {
            builder.ToTable(nameof(JobEducationLevel));
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.EducationLevelId)
                .HasMaxLength(24)
                .IsRequired();
            
            builder.HasIndex(p => p.EducationLevelId);
        }
    }
}