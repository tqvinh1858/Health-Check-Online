using BHEP.Contract.Constants;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.BlogEntitiesConfiguration;
internal class BlogPhotoConfiguration : IEntityTypeConfiguration<BlogPhoto>
{
    public void Configure(EntityTypeBuilder<BlogPhoto> builder)
    {
        builder.ToTable(TableName.BlogPhoto);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Blog)
            .WithMany(t => t.BlogPhotos)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
