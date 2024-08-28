using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddTableDevice : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 20, 17, 2, 34, 290, DateTimeKind.Local).AddTicks(2438),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7520));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 20, 17, 2, 34, 290, DateTimeKind.Local).AddTicks(2066),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7112));

        migrationBuilder.CreateTable(
            name: "Device",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProductId = table.Column<int>(type: "int", nullable: false),
                TransactionId = table.Column<int>(type: "int", nullable: true),
                Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsSale = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Device", x => x.Id);
                table.ForeignKey(
                    name: "FK_Device_CoinTransaction_TransactionId",
                    column: x => x.TransactionId,
                    principalTable: "CoinTransaction",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Device_Product_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Product",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Device_ProductId",
            table: "Device",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_Device_TransactionId",
            table: "Device",
            column: "TransactionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Device");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7520),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 20, 17, 2, 34, 290, DateTimeKind.Local).AddTicks(2438));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 19, 18, 35, 24, 252, DateTimeKind.Local).AddTicks(7112),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 20, 17, 2, 34, 290, DateTimeKind.Local).AddTicks(2066));
    }
}
