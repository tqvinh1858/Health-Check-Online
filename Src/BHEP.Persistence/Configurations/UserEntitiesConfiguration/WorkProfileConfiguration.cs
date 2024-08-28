using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class WorkProfileConfiguration : IEntityTypeConfiguration<WorkProfile>
{
    public void Configure(EntityTypeBuilder<WorkProfile> builder)
    {
        builder.ToTable(TableName.WorkProfile);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<WorkProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Major)
            .WithMany(t => t.WorkProfiles)
            .HasForeignKey(x => x.MajorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
