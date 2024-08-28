using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class ChangeVoucherTransactionTableName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PaymentVoucher_CoinTransaction_TransactionId",
            table: "PaymentVoucher");

        migrationBuilder.DropForeignKey(
            name: "FK_PaymentVoucher_Voucher_VoucherId",
            table: "PaymentVoucher");

        migrationBuilder.DropPrimaryKey(
            name: "PK_PaymentVoucher",
            table: "PaymentVoucher");

        migrationBuilder.RenameTable(
            name: "PaymentVoucher",
            newName: "VoucherTransaction");

        migrationBuilder.RenameIndex(
            name: "IX_PaymentVoucher_VoucherId",
            table: "VoucherTransaction",
            newName: "IX_VoucherTransaction_VoucherId");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(9085),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 249, DateTimeKind.Local).AddTicks(200));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(8740),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 248, DateTimeKind.Local).AddTicks(9831));

        migrationBuilder.AddPrimaryKey(
            name: "PK_VoucherTransaction",
            table: "VoucherTransaction",
            columns: new[] { "TransactionId", "VoucherId" });

        migrationBuilder.AddForeignKey(
            name: "FK_VoucherTransaction_CoinTransaction_TransactionId",
            table: "VoucherTransaction",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_VoucherTransaction_Voucher_VoucherId",
            table: "VoucherTransaction",
            column: "VoucherId",
            principalTable: "Voucher",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_VoucherTransaction_CoinTransaction_TransactionId",
            table: "VoucherTransaction");

        migrationBuilder.DropForeignKey(
            name: "FK_VoucherTransaction_Voucher_VoucherId",
            table: "VoucherTransaction");

        migrationBuilder.DropPrimaryKey(
            name: "PK_VoucherTransaction",
            table: "VoucherTransaction");

        migrationBuilder.RenameTable(
            name: "VoucherTransaction",
            newName: "PaymentVoucher");

        migrationBuilder.RenameIndex(
            name: "IX_VoucherTransaction_VoucherId",
            table: "PaymentVoucher",
            newName: "IX_PaymentVoucher_VoucherId");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 249, DateTimeKind.Local).AddTicks(200),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(9085));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 248, DateTimeKind.Local).AddTicks(9831),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 19, 17, 958, DateTimeKind.Local).AddTicks(8740));

        migrationBuilder.AddPrimaryKey(
            name: "PK_PaymentVoucher",
            table: "PaymentVoucher",
            columns: new[] { "TransactionId", "VoucherId" });

        migrationBuilder.AddForeignKey(
            name: "FK_PaymentVoucher_CoinTransaction_TransactionId",
            table: "PaymentVoucher",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PaymentVoucher_Voucher_VoucherId",
            table: "PaymentVoucher",
            column: "VoucherId",
            principalTable: "Voucher",
            principalColumn: "Id");
    }
}
