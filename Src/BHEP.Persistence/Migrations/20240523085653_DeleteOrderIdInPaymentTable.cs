using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteOrderIdInPaymentTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "OrderId",
            table: "Payment");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(673),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6946));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(326),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6513));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6946),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(673));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6513),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(326));

        migrationBuilder.AddColumn<int>(
            name: "OrderId",
            table: "Payment",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }
}
