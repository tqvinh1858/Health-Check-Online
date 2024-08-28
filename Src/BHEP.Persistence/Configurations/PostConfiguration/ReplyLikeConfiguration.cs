using BHEP.Contract.Constants;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.PostConfiguration;
internal class ReplyLikeConfiguration : IEntityTypeConfiguration<ReplyLike>
{
    public void Configure(EntityTypeBuilder<ReplyLike> builder)
    {
        builder.ToTable(TableName.ReplyLike);
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.User)
            .WithMany(x => x.ReplyLikes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Reply)
            .WithMany(x => x.ReplyLikes)
            .HasForeignKey(x => x.ReplyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
