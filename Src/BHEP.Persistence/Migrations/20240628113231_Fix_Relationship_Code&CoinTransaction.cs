using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Fix_Relationship_CodeCoinTransaction : Migration
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
            defaultValue: new DateTime(2024, 6, 28, 18, 32, 29, 844, DateTimeKind.Local).AddTicks(7120),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 20, 17, 4, 25, 988, DateTimeKind.Local).AddTicks(8079));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 18, 32, 29, 844, DateTimeKind.Local).AddTicks(6788),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 20, 17, 4, 25, 988, DateTimeKind.Local).AddTicks(7695));

        migrationBuilder.AddColumn<int>(
            name: "CodeId",
            table: "CoinTransaction",
            type: "int",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            unique: true,
            filter: "[CodeId] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            principalTable: "Code",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "CodeId",
            table: "CoinTransaction");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 20, 17, 4, 25, 988, DateTimeKind.Local).AddTicks(8079),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 18, 32, 29, 844, DateTimeKind.Local).AddTicks(7120));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 20, 17, 4, 25, 988, DateTimeKind.Local).AddTicks(7695),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 18, 32, 29, 844, DateTimeKind.Local).AddTicks(6788));

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
}
