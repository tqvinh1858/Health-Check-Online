using BHEP.Contract.Constants;
using BHEP.Domain.Entities.BlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.BlogEntitiesConfiguration;
internal class BlogRateConfiguration : IEntityTypeConfiguration<BlogRate>
{
    public void Configure(EntityTypeBuilder<BlogRate> builder)
    {
        builder.ToTable(TableName.BlogRate);
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.User)
            .WithMany(x => x.BlogRates)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.BlogRates)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
