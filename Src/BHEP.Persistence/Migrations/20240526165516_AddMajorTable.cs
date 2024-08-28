using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddMajorTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Major",
            table: "JobApplication",
            newName: "MajorId");

        migrationBuilder.AddColumn<int>(
            name: "MajorId",
            table: "WorkProfile",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7510),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6521));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7186),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6043));

        migrationBuilder.CreateTable(
            name: "Major",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Major", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_WorkProfile_MajorId",
            table: "WorkProfile",
            column: "MajorId");

        migrationBuilder.CreateIndex(
            name: "IX_Major_Name",
            table: "Major",
            column: "Name",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_WorkProfile_Major_MajorId",
            table: "WorkProfile",
            column: "MajorId",
            principalTable: "Major",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_WorkProfile_Major_MajorId",
            table: "WorkProfile");

        migrationBuilder.DropTable(
            name: "Major");

        migrationBuilder.DropIndex(
            name: "IX_WorkProfile_MajorId",
            table: "WorkProfile");

        migrationBuilder.DropColumn(
            name: "MajorId",
            table: "WorkProfile");

        migrationBuilder.RenameColumn(
            name: "MajorId",
            table: "JobApplication",
            newName: "Major");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6521),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7510));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6043),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7186));
    }
}
