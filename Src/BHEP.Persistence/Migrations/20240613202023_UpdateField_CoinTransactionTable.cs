using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateField_CoinTransactionTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropForeignKey(
            name: "FK_CoinTransaction_Voucher_VoucherId",
            table: "CoinTransaction");

        migrationBuilder.DropIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropIndex(
            name: "IX_CoinTransaction_VoucherId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "VoucherId",
            table: "CoinTransaction");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8486),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(1336));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8153),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(988));

        migrationBuilder.CreateIndex(
            name: "IX_Code_TransactionId",
            table: "Code",
            column: "TransactionId");

        migrationBuilder.AddForeignKey(
            name: "FK_Code_CoinTransaction_TransactionId",
            table: "Code",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Code_CoinTransaction_TransactionId",
            table: "Code");

        migrationBuilder.DropIndex(
            name: "IX_Code_TransactionId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(1336),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8486));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 23, 18, 5, 226, DateTimeKind.Local).AddTicks(988),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8153));

        migrationBuilder.AddColumn<int>(
            name: "CodeId",
            table: "CoinTransaction",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "VoucherId",
            table: "CoinTransaction",
            type: "int",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            unique: true,
            filter: "[CodeId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_CoinTransaction_VoucherId",
            table: "CoinTransaction",
            column: "VoucherId");

        migrationBuilder.AddForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            principalTable: "Code",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_CoinTransaction_Voucher_VoucherId",
            table: "CoinTransaction",
            column: "VoucherId",
            principalTable: "Voucher",
            principalColumn: "Id");
    }
}
