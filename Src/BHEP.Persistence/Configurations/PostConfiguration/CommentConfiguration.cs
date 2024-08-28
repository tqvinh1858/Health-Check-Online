using BHEP.Contract.Constants;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.PostConfiguration;
internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(TableName.Comment);
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.User)
            .WithMany(t => t.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Post)
            .WithMany(t => t.Comments)
            .HasForeignKey(t => t.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.CommentLikes)
            .WithOne(t => t.Comment)
            .HasForeignKey(t => t.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Replies)
            .WithOne(t => t.Comment)
            .HasForeignKey(t => t.CommentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
