using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdatePaymentTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Date",
            table: "Payment");

        migrationBuilder.AddColumn<decimal>(
            name: "Amount",
            table: "Payment",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(5901));

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Payment",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "Payment",
            type: "int",
            nullable: false,
            defaultValue: 1);

        migrationBuilder.AddColumn<string>(
            name: "TransactionId",
            table: "Payment",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 21, 58, 39, 811, DateTimeKind.Local).AddTicks(6249));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Amount",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "Status",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "TransactionId",
            table: "Payment");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Payment");

        migrationBuilder.AddColumn<DateTime>(
            name: "Date",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}
