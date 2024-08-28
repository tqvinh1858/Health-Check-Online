using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class CoinTransactionConfiguration : IEntityTypeConfiguration<CoinTransaction>
{
    public void Configure(EntityTypeBuilder<CoinTransaction> builder)
    {
        builder.ToTable(TableName.CoinTransaction);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount).HasDefaultValue(0);

        builder.HasOne(x => x.User)
            .WithMany(t => t.CoinTransactions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Code)
            .WithOne(t => t.Transaction)
            .HasForeignKey<Code>(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.VoucherTransaction)
            .WithOne(x => x.Transaction)
            .HasForeignKey(t => t.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ProductTransactions)
            .WithOne(x => x.Transaction)
            .HasForeignKey(t => t.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ServiceTransactions)
            .WithOne(x => x.Transaction)
            .HasForeignKey(t => t.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Devices)
            .WithOne(t => t.Transaction)
            .HasForeignKey(t => t.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
