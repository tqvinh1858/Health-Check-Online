using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Add_HealthRecordTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_User_Appointment_AppointmentId",
            table: "User");

        migrationBuilder.DropIndex(
            name: "IX_User_AppointmentId",
            table: "User");

        migrationBuilder.DropColumn(
            name: "AppointmentId",
            table: "User");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 30, 12, 6, 32, 754, DateTimeKind.Local).AddTicks(8765),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(2355));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 30, 12, 6, 32, 754, DateTimeKind.Local).AddTicks(8382),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(1782));

        migrationBuilder.CreateTable(
            name: "HealthRecord",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                DeviceId = table.Column<int>(type: "int", nullable: false),
                Temp = table.Column<float>(type: "real", nullable: false),
                HeartBeat = table.Column<float>(type: "real", nullable: false),
                ESpO2 = table.Column<float>(type: "real", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HealthRecord", x => x.Id);
                table.ForeignKey(
                    name: "FK_HealthRecord_Device_DeviceId",
                    column: x => x.DeviceId,
                    principalTable: "Device",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_HealthRecord_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_HealthRecord_DeviceId",
            table: "HealthRecord",
            column: "DeviceId");

        migrationBuilder.CreateIndex(
            name: "IX_HealthRecord_UserId",
            table: "HealthRecord",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "HealthRecord");

        migrationBuilder.AddColumn<int>(
            name: "AppointmentId",
            table: "User",
            type: "int",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(2355),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 30, 12, 6, 32, 754, DateTimeKind.Local).AddTicks(8765));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 28, 19, 5, 30, 361, DateTimeKind.Local).AddTicks(1782),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 30, 12, 6, 32, 754, DateTimeKind.Local).AddTicks(8382));

        migrationBuilder.CreateIndex(
            name: "IX_User_AppointmentId",
            table: "User",
            column: "AppointmentId");

        migrationBuilder.AddForeignKey(
            name: "FK_User_Appointment_AppointmentId",
            table: "User",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id");
    }
}
