using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteRelationShipCodeAndService : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Code_Service_ServiceId",
            table: "Code");

        migrationBuilder.DropIndex(
            name: "IX_Code_ServiceId",
            table: "Code");

        migrationBuilder.DropColumn(
            name: "ServiceId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(2705),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5409));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(1942),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5074));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5409),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(2705));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5074),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 12, 1, 22, 311, DateTimeKind.Local).AddTicks(1942));

        migrationBuilder.AddColumn<int>(
            name: "ServiceId",
            table: "Code",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Code_ServiceId",
            table: "Code",
            column: "ServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_Code_Service_ServiceId",
            table: "Code",
            column: "ServiceId",
            principalTable: "Service",
            principalColumn: "Id");
    }
}
