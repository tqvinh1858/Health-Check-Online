using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DbInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "GeoLocation",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GeoLocation", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Specialist",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specialist", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Symptom",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Symptom", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<int>(type: "int", nullable: false),
                GeoLocationId = table.Column<int>(type: "int", nullable: false),
                SpecialistId = table.Column<int>(type: "int", nullable: true),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Identify = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Gender = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_GeoLocation_GeoLocationId",
                    column: x => x.GeoLocationId,
                    principalTable: "GeoLocation",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_User_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_User_Specialist_SpecialistId",
                    column: x => x.SpecialistId,
                    principalTable: "Specialist",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "SpecialistSymptom",
            columns: table => new
            {
                SpecialistId = table.Column<int>(type: "int", nullable: false),
                SymptomId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SpecialistSymptom", x => new { x.SymptomId, x.SpecialistId });
                table.ForeignKey(
                    name: "FK_SpecialistSymptom_Specialist_SpecialistId",
                    column: x => x.SpecialistId,
                    principalTable: "Specialist",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_SpecialistSymptom_Symptom_SymptomId",
                    column: x => x.SymptomId,
                    principalTable: "Symptom",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Appointment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(type: "int", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointment_User_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "HistoryAppointment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(type: "int", nullable: false),
                EmployeeId = table.Column<int>(type: "int", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HistoryAppointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_HistoryAppointment_User_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "User",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_HistoryAppointment_User_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "JobApplication",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(type: "int", nullable: false),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Major = table.Column<int>(type: "int", nullable: false),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExperienceYear = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JobApplication", x => x.Id);
                table.ForeignKey(
                    name: "FK_JobApplication_User_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "AppointmentSymptom",
            columns: table => new
            {
                AppointmentId = table.Column<int>(type: "int", nullable: false),
                SymptomId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppointmentSymptom", x => new { x.SymptomId, x.AppointmentId });
                table.ForeignKey(
                    name: "FK_AppointmentSymptom_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_AppointmentSymptom_Symptom_SymptomId",
                    column: x => x.SymptomId,
                    principalTable: "Symptom",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_CustomerId",
            table: "Appointment",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_AppointmentSymptom_AppointmentId",
            table: "AppointmentSymptom",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_HistoryAppointment_CustomerId",
            table: "HistoryAppointment",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_HistoryAppointment_EmployeeId",
            table: "HistoryAppointment",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_JobApplication_CustomerId",
            table: "JobApplication",
            column: "CustomerId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Role_Name",
            table: "Role",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Specialist_Name",
            table: "Specialist",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SpecialistSymptom_SpecialistId",
            table: "SpecialistSymptom",
            column: "SpecialistId");

        migrationBuilder.CreateIndex(
            name: "IX_Symptom_Name",
            table: "Symptom",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_Email",
            table: "User",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_GeoLocationId",
            table: "User",
            column: "GeoLocationId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_Identify",
            table: "User",
            column: "Identify",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_PhoneNumber",
            table: "User",
            column: "PhoneNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_RoleId",
            table: "User",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_User_SpecialistId",
            table: "User",
            column: "SpecialistId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AppointmentSymptom");

        migrationBuilder.DropTable(
            name: "HistoryAppointment");

        migrationBuilder.DropTable(
            name: "JobApplication");

        migrationBuilder.DropTable(
            name: "SpecialistSymptom");

        migrationBuilder.DropTable(
            name: "Appointment");

        migrationBuilder.DropTable(
            name: "Symptom");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "GeoLocation");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "Specialist");
    }
}
