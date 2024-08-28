using BHEP.Contract.Constants;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.PostConfiguration;
internal class ReplyConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder.ToTable(TableName.Reply);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(t => t.Replies)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Comment)
            .WithMany(t => t.Replies)
            .HasForeignKey(x => x.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ReplyLikes)
            .WithOne(t => t.Reply)
            .HasForeignKey(x => x.ReplyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
