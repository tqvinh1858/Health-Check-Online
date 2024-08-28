using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Fix_Relationship_DeviceCoinTransaction : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Code_TransactionId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(2355),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 18, 46, 52, 706, DateTimeKind.Local).AddTicks(43));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(1782),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 18, 46, 52, 705, DateTimeKind.Local).AddTicks(8927));

        migrationBuilder.CreateIndex(
            name: "IX_Code_TransactionId",
            table: "Code",
            column: "TransactionId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Code_TransactionId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 18, 46, 52, 706, DateTimeKind.Local).AddTicks(43),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(2355));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 18, 46, 52, 705, DateTimeKind.Local).AddTicks(8927),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(1782));

        migrationBuilder.CreateIndex(
            name: "IX_Code_TransactionId",
            table: "Code",
            column: "TransactionId");
    }
}
