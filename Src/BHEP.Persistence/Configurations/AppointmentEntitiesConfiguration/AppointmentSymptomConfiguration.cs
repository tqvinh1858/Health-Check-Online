using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.AppointmentEntitiesConfiguration;
internal class AppointmentSymptomConfiguration : IEntityTypeConfiguration<AppointmentSymptom>
{
    public void Configure(EntityTypeBuilder<AppointmentSymptom> builder)
    {
        builder.ToTable(TableName.AppointmentSymptom);
        builder.HasKey(x => new { x.SymptomId, x.AppointmentId });

        builder.HasOne(x => x.Symptom)
            .WithMany(t => t.AppointmentSymptoms)
            .HasForeignKey(x => x.SymptomId);

        builder.HasOne(x => x.Appointment)
            .WithMany(t => t.AppointmentSymptoms)
            .HasForeignKey(x => x.AppointmentId);
    }
}
