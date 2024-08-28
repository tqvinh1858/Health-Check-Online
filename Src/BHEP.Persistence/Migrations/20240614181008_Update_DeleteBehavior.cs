using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_DeleteBehavior : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppointmentSymptom_Appointment_AppointmentId",
            table: "AppointmentSymptom");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5702),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 0, 58, 24, 265, DateTimeKind.Local).AddTicks(253));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5385),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 0, 58, 24, 264, DateTimeKind.Local).AddTicks(9933));

        migrationBuilder.AddForeignKey(
            name: "FK_AppointmentSymptom_Appointment_AppointmentId",
            table: "AppointmentSymptom",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppointmentSymptom_Appointment_AppointmentId",
            table: "AppointmentSymptom");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 0, 58, 24, 265, DateTimeKind.Local).AddTicks(253),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5702));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 0, 58, 24, 264, DateTimeKind.Local).AddTicks(9933),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 1, 10, 6, 347, DateTimeKind.Local).AddTicks(5385));

        migrationBuilder.AddForeignKey(
            name: "FK_AppointmentSymptom_Appointment_AppointmentId",
            table: "AppointmentSymptom",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id");
    }
}
