using BHEP.Contract.Constants;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.BlogEntitiesConfiguration;
internal class BlogTopicConfiguration : IEntityTypeConfiguration<BlogTopic>
{
    public void Configure(EntityTypeBuilder<BlogTopic> builder)
    {
        builder.ToTable(TableName.BlogTopic);
        builder.HasKey(x => new { x.BlogId, x.TopicId });

        builder.HasOne(x => x.Blog)
            .WithMany(t => t.BlogTopics)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Topic)
            .WithMany(t => t.BlogTopics)
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
