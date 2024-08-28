using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class CodeRelationShip : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5409),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9981));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5074),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9665));

        migrationBuilder.AddColumn<int>(
            name: "UserId",
            table: "Code",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "UserCode",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false),
                CodeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserCode", x => new { x.UserId, x.CodeId });
                table.ForeignKey(
                    name: "FK_UserCode_Code_CodeId",
                    column: x => x.CodeId,
                    principalTable: "Code",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserCode_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Code_UserId",
            table: "Code",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserCode_CodeId",
            table: "UserCode",
            column: "CodeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Code_User_UserId",
            table: "Code",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Code_User_UserId",
            table: "Code");

        migrationBuilder.DropTable(
            name: "UserCode");

        migrationBuilder.DropIndex(
            name: "IX_Code_UserId",
            table: "Code");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Code");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9981),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5409));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 21, 23, 46, 15, 216, DateTimeKind.Local).AddTicks(9665),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 11, 31, 19, 560, DateTimeKind.Local).AddTicks(5074));
    }
}
