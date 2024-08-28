using BHEP.Contract.Constants;
using BHEP.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.PostConfiguration;
internal class PostSpecialistConfiguration : IEntityTypeConfiguration<PostSpecialist>
{
    public void Configure(EntityTypeBuilder<PostSpecialist> builder)
    {
        builder.ToTable(TableName.PostSpecialist);

        builder.HasKey(x => new { x.PostId, x.SpecialistId });

        builder.HasOne(x => x.Specialist)
            .WithMany(t => t.PostSpecialists)
            .HasForeignKey(x => x.SpecialistId);

        builder.HasOne(x => x.Post)
            .WithMany(t => t.PostSpecialists)
            .HasForeignKey(x => x.PostId);
    }
}
