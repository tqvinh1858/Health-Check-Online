using BHEP.Contract.Constants;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.UserEntitiesConfiguration;
internal class GeoLocationConfiguration : IEntityTypeConfiguration<GeoLocation>
{
    public void Configure(EntityTypeBuilder<GeoLocation> builder)
    {
        builder.ToTable(TableName.GeoLocation);
        builder.HasKey(x => x.Id);
    }
}
