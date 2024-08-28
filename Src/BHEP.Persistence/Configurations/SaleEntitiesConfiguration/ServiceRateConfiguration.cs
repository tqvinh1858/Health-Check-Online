using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class ServiceRateConfiguration : IEntityTypeConfiguration<ServiceRate>
{
    public void Configure(EntityTypeBuilder<ServiceRate> builder)
    {
        builder.ToTable(TableName.ServiceRate);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(t => t.ServiceRates)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Service)
            .WithMany(t => t.ServiceRates)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Transaction)
            .WithOne()
            .HasForeignKey<ServiceRate>(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
