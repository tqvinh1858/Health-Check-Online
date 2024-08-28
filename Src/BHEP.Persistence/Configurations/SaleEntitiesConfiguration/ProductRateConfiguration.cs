using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class ProductRateConfiguration : IEntityTypeConfiguration<ProductRate>
{
    public void Configure(EntityTypeBuilder<ProductRate> builder)
    {
        builder.ToTable(TableName.ProductRate);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(t => t.ProductRates)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Product)
            .WithMany(t => t.ProductRates)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Transaction)
            .WithOne()
            .HasForeignKey<ProductRate>(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
