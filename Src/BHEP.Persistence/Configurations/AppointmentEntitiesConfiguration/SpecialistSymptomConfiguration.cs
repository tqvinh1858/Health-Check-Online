using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.AppointmentEntitiesConfiguration;
internal class SpecialistSymptomConfiguration : IEntityTypeConfiguration<SpecialistSymptom>
{
    public void Configure(EntityTypeBuilder<SpecialistSymptom> builder)
    {
        builder.ToTable(TableName.SpecialistSymptom);
        builder.HasKey(x => new { x.SymptomId, x.SpecialistId });

        builder.HasOne(x => x.Symptom)
            .WithMany(t => t.SpecialistSymptoms)
            .HasForeignKey(x => x.SymptomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Specialist)
            .WithMany(t => t.SpecialistsSymptom)
            .HasForeignKey(x => x.SpecialistId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
