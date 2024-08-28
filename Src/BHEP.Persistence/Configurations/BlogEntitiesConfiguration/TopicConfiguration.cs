using BHEP.Contract.Constants;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.BlogEntitiesConfiguration;
internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable(TableName.Topic);
        builder.HasKey(x => x.Id);

        // Unique
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.BlogTopics)
            .WithOne(t => t.Topic)
            .HasForeignKey(t => t.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
