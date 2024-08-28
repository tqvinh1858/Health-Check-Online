using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateFieldPayment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9981),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(6249));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9665),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(5901));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Payment",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Payment",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Payment");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(6249),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9981));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(5901),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9665));
    }
}
