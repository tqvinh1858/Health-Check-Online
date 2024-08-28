using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable(TableName.Schedule);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Employee)
            .WithMany(u => u.Schedules)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
