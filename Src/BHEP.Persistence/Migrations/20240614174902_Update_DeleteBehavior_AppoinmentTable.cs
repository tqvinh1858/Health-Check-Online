using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_DeleteBehavior_AppoinmentTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppointmentSymptom_Symptom_SymptomId",
            table: "AppointmentSymptom");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 0, 49, 1, 23, DateTimeKind.Local).AddTicks(835),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8486));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 15, 0, 49, 1, 23, DateTimeKind.Local).AddTicks(322),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8153));

        migrationBuilder.AddForeignKey(
            name: "FK_AppointmentSymptom_Symptom_SymptomId",
            table: "AppointmentSymptom",
            column: "SymptomId",
            principalTable: "Symptom",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppointmentSymptom_Symptom_SymptomId",
            table: "AppointmentSymptom");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8486),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 0, 49, 1, 23, DateTimeKind.Local).AddTicks(835));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 14, 3, 20, 23, 171, DateTimeKind.Local).AddTicks(8153),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 15, 0, 49, 1, 23, DateTimeKind.Local).AddTicks(322));

        migrationBuilder.AddForeignKey(
            name: "FK_AppointmentSymptom_Symptom_SymptomId",
            table: "AppointmentSymptom",
            column: "SymptomId",
            principalTable: "Symptom",
            principalColumn: "Id");
    }
}
