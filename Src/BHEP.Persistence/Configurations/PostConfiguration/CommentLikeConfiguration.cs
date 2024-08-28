using BHEP.Contract.Constants;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.PostConfiguration;
internal class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.ToTable(TableName.CommentLike);
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.User)
            .WithMany(t => t.CommentLikes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Comment)
            .WithMany(t => t.CommentLikes)
            .HasForeignKey(x => x.CommentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
