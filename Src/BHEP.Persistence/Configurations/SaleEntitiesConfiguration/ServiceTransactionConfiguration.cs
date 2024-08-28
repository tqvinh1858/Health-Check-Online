using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class ServiceTransactionServiceDetailConfiguration : IEntityTypeConfiguration<ServiceTransaction>
{
    public void Configure(EntityTypeBuilder<ServiceTransaction> builder)
    {
        builder.ToTable(TableName.ServiceTransaction);
        builder.HasKey(x => new { x.TransactionId, x.ServiceId });


        builder.HasOne(x => x.Service)
            .WithMany(t => t.ServiceTransactions)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Transaction)
            .WithMany(t => t.ServiceTransactions)
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
