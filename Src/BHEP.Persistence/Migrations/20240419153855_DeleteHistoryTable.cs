using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteHistoryTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "HistoryAppointment");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Appointment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Appointment",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "EmployeeId",
            table: "Appointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Appointment",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "Appointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Appointment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_EmployeeId",
            table: "Appointment",
            column: "EmployeeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_User_EmployeeId",
            table: "Appointment",
            column: "EmployeeId",
            principalTable: "User",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_User_EmployeeId",
            table: "Appointment");

        migrationBuilder.DropIndex(
            name: "IX_Appointment_EmployeeId",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "EmployeeId",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "Status",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Appointment");

        migrationBuilder.CreateTable(
            name: "HistoryAppointment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CustomerId = table.Column<int>(type: "int", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                EmployeeId = table.Column<int>(type: "int", nullable: false),
                Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HistoryAppointment", x => x.Id);
            });
    }
}
