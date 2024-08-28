using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_DateField_Appoinment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4434),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 12, 21, 50, 46, 339, DateTimeKind.Local).AddTicks(3750));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4019),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 12, 21, 50, 46, 339, DateTimeKind.Local).AddTicks(3241));

        migrationBuilder.AlterColumn<DateTime>(
            name: "Date",
            table: "Appointment",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 12, 21, 50, 46, 339, DateTimeKind.Local).AddTicks(3750),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4434));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 12, 21, 50, 46, 339, DateTimeKind.Local).AddTicks(3241),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4019));

        migrationBuilder.AlterColumn<string>(
            name: "Date",
            table: "Appointment",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }
}
