using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableName.Payment);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).IsRequired().HasDefaultValue(PaymentStatus.Pending);
        builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(x => x.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        builder.HasOne(x => x.User)
            .WithMany(t => t.Payments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
