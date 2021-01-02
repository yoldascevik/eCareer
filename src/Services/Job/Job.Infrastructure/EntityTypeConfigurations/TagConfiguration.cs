using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.EntityTypeConfigurations
{
    public class TagConfiguration: IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag));
            builder.HasKey(t => t.Id);
            builder.HasIndex(p => p.Name);
        }
    }
}