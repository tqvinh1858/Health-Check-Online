using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable(TableName.Voucher);

        builder.HasMany(x => x.UserVouchers)
            .WithOne(t => t.Voucher)
            .HasForeignKey(t => t.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.VoucherTransactions)
            .WithOne(t => t.Voucher)
            .HasForeignKey(x => x.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
