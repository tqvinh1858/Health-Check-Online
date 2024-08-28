using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class FixName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CancleReason",
            table: "JobApplication",
            newName: "CancelReason");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CancelReason",
            table: "JobApplication",
            newName: "CancleReason");
    }
}
