using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class RelationShipCodeAndOrder : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6946),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(2705));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6513),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(1942));

        migrationBuilder.AddColumn<int>(
            name: "CodeId",
            table: "Order",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "OrderId",
            table: "Code",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Order_CodeId",
            table: "Order",
            column: "CodeId",
            unique: true,
            filter: "[CodeId] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_Order_Code_CodeId",
            table: "Order",
            column: "CodeId",
            principalTable: "Code",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Order_Code_CodeId",
            table: "Order");

        migrationBuilder.DropIndex(
            name: "IX_Order_CodeId",
            table: "Order");

        migrationBuilder.DropColumn(
            name: "CodeId",
            table: "Order");

        migrationBuilder.DropColumn(
            name: "OrderId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(2705),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6946));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(1942),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 14, 48, 41, 913, DateTimeKind.Local).AddTicks(6513));
    }
}
