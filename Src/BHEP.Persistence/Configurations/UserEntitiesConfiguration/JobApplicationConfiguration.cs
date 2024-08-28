using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable(TableName.JobApplication);
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithOne()
            .HasForeignKey<JobApplication>(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
