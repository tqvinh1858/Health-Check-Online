using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserCodeConfiguration : IEntityTypeConfiguration<UserCode>
{
    public void Configure(EntityTypeBuilder<UserCode> builder)
    {
        builder.ToTable(TableName.UserCode);

        builder.HasKey(x => new { x.UserId, x.CodeId });

        builder.HasOne(x => x.User)
            .WithMany(t => t.UserCodes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Code)
            .WithMany(t => t.UserCodes)
            .HasForeignKey(x => x.CodeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
