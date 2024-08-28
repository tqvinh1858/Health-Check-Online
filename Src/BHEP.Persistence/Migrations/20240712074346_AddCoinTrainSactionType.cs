using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddCoinTrainSactionType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 12, 14, 43, 44, 842, DateTimeKind.Local).AddTicks(3449),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1676));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 12, 14, 43, 44, 842, DateTimeKind.Local).AddTicks(3158),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1209));

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "CoinTransaction",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Type",
            table: "CoinTransaction");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1676),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 12, 14, 43, 44, 842, DateTimeKind.Local).AddTicks(3449));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1209),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 12, 14, 43, 44, 842, DateTimeKind.Local).AddTicks(3158));
    }
}
