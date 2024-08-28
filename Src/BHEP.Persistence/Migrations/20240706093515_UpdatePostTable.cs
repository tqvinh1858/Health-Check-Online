using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdatePostTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Post_Specialist_UserId",
            table: "Post");

        migrationBuilder.DropColumn(
            name: "SpecialistId",
            table: "Post");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1676),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 3, 14, 5, 17, 701, DateTimeKind.Local).AddTicks(4810));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1209),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 3, 14, 5, 17, 701, DateTimeKind.Local).AddTicks(3581));

        migrationBuilder.CreateTable(
            name: "PostSpecialist",
            columns: table => new
            {
                PostId = table.Column<int>(type: "int", nullable: false),
                SpecialistId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PostSpecialist", x => new { x.PostId, x.SpecialistId });
                table.ForeignKey(
                    name: "FK_PostSpecialist_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PostSpecialist_Specialist_SpecialistId",
                    column: x => x.SpecialistId,
                    principalTable: "Specialist",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PostSpecialist_SpecialistId",
            table: "PostSpecialist",
            column: "SpecialistId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PostSpecialist");

        migrationBuilder.AddColumn<int>(
            name: "SpecialistId",
            table: "Post",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 3, 14, 5, 17, 701, DateTimeKind.Local).AddTicks(4810),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1676));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 7, 3, 14, 5, 17, 701, DateTimeKind.Local).AddTicks(3581),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 7, 6, 16, 35, 14, 167, DateTimeKind.Local).AddTicks(1209));

        migrationBuilder.AddForeignKey(
            name: "FK_Post_Specialist_UserId",
            table: "Post",
            column: "UserId",
            principalTable: "Specialist",
            principalColumn: "Id");
    }
}
