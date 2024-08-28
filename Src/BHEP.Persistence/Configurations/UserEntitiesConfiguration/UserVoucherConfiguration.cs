using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class UserVoucherConfiguration : IEntityTypeConfiguration<UserVoucher>
{
    public void Configure(EntityTypeBuilder<UserVoucher> builder)
    {
        builder.ToTable(TableName.UserVoucher);

        builder.HasKey(x => new { x.UserId, x.VoucherId });

        builder.HasOne(x => x.User)
            .WithMany(t => t.UserVouchers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Voucher)
            .WithMany(t => t.UserVouchers)
            .HasForeignKey(x => x.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
