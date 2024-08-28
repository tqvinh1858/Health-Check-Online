using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.AppointmentEntitiesConfiguration;
internal class SymptomConfiguration : IEntityTypeConfiguration<Symptom>
{
    public void Configure(EntityTypeBuilder<Symptom> builder)
    {
        builder.ToTable(TableName.Symptom);
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.AppointmentSymptoms)
            .WithOne(t => t.Symptom)
            .HasForeignKey(t => t.SymptomId);

        builder.HasMany(x => x.SpecialistSymptoms)
            .WithOne(t => t.Symptom)
            .HasForeignKey(t => t.SymptomId);
    }
}
