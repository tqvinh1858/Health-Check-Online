using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddNulable_CoinTransactionTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(1336),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(9085));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(988),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(8740));

        migrationBuilder.AlterColumn<int>(
            name: "VoucherId",
            table: "CoinTransaction",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "CoinTransaction",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateIndex(
            name: "IX_CoinTransaction_VoucherId",
            table: "CoinTransaction",
            column: "VoucherId");

        migrationBuilder.AddForeignKey(
            name: "FK_CoinTransaction_Voucher_VoucherId",
            table: "CoinTransaction",
            column: "VoucherId",
            principalTable: "Voucher",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CoinTransaction_Voucher_VoucherId",
            table: "CoinTransaction");

        migrationBuilder.DropIndex(
            name: "IX_CoinTransaction_VoucherId",
            table: "CoinTransaction");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(9085),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(1336));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(8740),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(988));

        migrationBuilder.AlterColumn<int>(
            name: "VoucherId",
            table: "CoinTransaction",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "CoinTransaction",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
    }
}
