using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class DeletionRequestConfiguration : IEntityTypeConfiguration<DeletionRequest>
{
    public void Configure(EntityTypeBuilder<DeletionRequest> builder)
    {
        builder.ToTable(TableName.DeletionRequest);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<DeletionRequest>(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
