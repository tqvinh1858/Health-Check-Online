using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class UserRateConfiguration : IEntityTypeConfiguration<UserRate>
{
    public void Configure(EntityTypeBuilder<UserRate> builder)
    {
        builder.ToTable(TableName.UserRate);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.GivenRates)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.ReceivedRates)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Appointment)
            .WithOne(t => t.UserRate)
            .HasForeignKey<UserRate>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
