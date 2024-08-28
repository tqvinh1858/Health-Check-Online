using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class ProductTransactionConfiguration : IEntityTypeConfiguration<ProductTransaction>
{
    public void Configure(EntityTypeBuilder<ProductTransaction> builder)
    {
        builder.ToTable(TableName.ProductTransaction);
        builder.HasKey(x => new { x.TransactionId, x.ProductId });

        builder.HasOne(x => x.Product)
            .WithMany(t => t.ProductTransactions)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Transaction)
            .WithMany(t => t.ProductTransactions)
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
