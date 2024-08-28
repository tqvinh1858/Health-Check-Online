using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteRelationshipUserHistoryAppointment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_HistoryAppointment_User_CustomerId",
            table: "HistoryAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_HistoryAppointment_User_EmployeeId",
            table: "HistoryAppointment");

        migrationBuilder.DropIndex(
            name: "IX_HistoryAppointment_CustomerId",
            table: "HistoryAppointment");

        migrationBuilder.DropIndex(
            name: "IX_HistoryAppointment_EmployeeId",
            table: "HistoryAppointment");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_HistoryAppointment_CustomerId",
            table: "HistoryAppointment",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_HistoryAppointment_EmployeeId",
            table: "HistoryAppointment",
            column: "EmployeeId");

        migrationBuilder.AddForeignKey(
            name: "FK_HistoryAppointment_User_CustomerId",
            table: "HistoryAppointment",
            column: "CustomerId",
            principalTable: "User",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_HistoryAppointment_User_EmployeeId",
            table: "HistoryAppointment",
            column: "EmployeeId",
            principalTable: "User",
            principalColumn: "Id");
    }
}
