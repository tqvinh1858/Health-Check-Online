using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateConfigCodeCoinTransaction : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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
            defaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7520),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5702));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7112),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5385));

        migrationBuilder.CreateIndex(
            name: "IX_Code_TransactionId",
            table: "Code",
            column: "TransactionId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Code_CoinTransaction_TransactionId",
            table: "Code",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");
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
            defaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5702),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7520));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5385),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7112));

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
}
