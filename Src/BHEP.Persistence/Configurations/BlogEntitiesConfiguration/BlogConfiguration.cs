using BHEP.Contract.Constants;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.BlogEntitiesConfiguration;
internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable(TableName.Blog);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(t => t.Blogs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.BlogTopics)
            .WithOne(t => t.Blog)
            .HasForeignKey(t => t.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.BlogRates)
            .WithOne(t => t.Blog)
            .HasForeignKey(t => t.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.BlogPhotos)
            .WithOne(t => t.Blog)
            .HasForeignKey(t => t.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
