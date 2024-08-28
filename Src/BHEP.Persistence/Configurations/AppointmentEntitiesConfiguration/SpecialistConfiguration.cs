using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.AppointmentEntitiesConfiguration;
internal class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
{
    public void Configure(EntityTypeBuilder<Specialist> builder)
    {
        builder.ToTable(TableName.Specialist);
        builder.HasKey(x => x.Id);

        // Unique
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.SpecialistsSymptom)
            .WithOne(t => t.Specialist)
            .HasForeignKey(t => t.SpecialistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
