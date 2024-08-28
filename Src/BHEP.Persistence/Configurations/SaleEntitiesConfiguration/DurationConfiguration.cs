using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class DurationConfiguration : IEntityTypeConfiguration<Duration>
{
    public void Configure(EntityTypeBuilder<Duration> builder)
    {
        builder.ToTable(TableName.Duration);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Service)
            .WithMany(t => t.Durations)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
            .WithMany(t => t.Durations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
