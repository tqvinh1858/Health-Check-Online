using BHEP.Contract.Constants;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations.SaleEntitiesConfiguration;
internal class CodeConfiguration : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
    {
        builder.ToTable(TableName.Code);
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.User)
            .WithMany(t => t.Codes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Transaction)
            .WithOne(t => t.Code)
            .HasForeignKey<Code>(x => x.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.UserCodes)
            .WithOne(t => t.Code)
            .HasForeignKey(t => t.CodeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
