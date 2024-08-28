using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class VoucherTransactionConfiguration : IEntityTypeConfiguration<VoucherTransaction>
{
    public void Configure(EntityTypeBuilder<VoucherTransaction> builder)
    {
        builder.ToTable(TableName.VoucherTransaction);
        builder.HasKey(x => new { x.TransactionId, x.VoucherId });

        builder.HasOne(x => x.Transaction)
            .WithMany(t => t.VoucherTransaction)
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Voucher)
            .WithMany(t => t.VoucherTransactions)
            .HasForeignKey(x => x.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
