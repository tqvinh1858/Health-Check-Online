using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteIdentifyInUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_User_Identify",
            table: "User");

        migrationBuilder.DropIndex(
            name: "IX_User_PhoneNumber",
            table: "User");

        migrationBuilder.DropColumn(
            name: "Identify",
            table: "User");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "User",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<string>(
            name: "Identify",
            table: "User",
            type: "nvarchar(450)",
            nullable: false,
            defaultValue: "");

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
    }
}
