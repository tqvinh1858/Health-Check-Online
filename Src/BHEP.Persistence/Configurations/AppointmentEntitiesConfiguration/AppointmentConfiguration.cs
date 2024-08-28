using BHEP.Contract.Constants;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.AppointmentEntitiesConfiguration;
internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable(TableName.Appointment);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price).HasDefaultValue(0).IsRequired();


        builder.HasMany(x => x.AppointmentSymptoms)
            .WithOne(t => t.Appointment)
            .HasForeignKey(t => t.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Customer)
            .WithMany(t => t.AppointmentCustomers)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Employee)
            .WithMany(t => t.AppointmentEmployees)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.UserRate)
            .WithOne(t => t.Appointment)
            .HasForeignKey<UserRate>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
