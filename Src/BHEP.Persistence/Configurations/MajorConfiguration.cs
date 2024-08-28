using BHEP.Contract.Constants;
using BHEP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHEP.Persistence.Configurations;
internal class MajorConfiguration : IEntityTypeConfiguration<Major>
{
    public void Configure(EntityTypeBuilder<Major> builder)
    {
        builder.ToTable(TableName.Major);
        builder.HasKey(x => x.Id);

        // Unique
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.WorkProfiles)
            .WithOne(t => t.Major)
            .HasForeignKey(t => t.MajorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
