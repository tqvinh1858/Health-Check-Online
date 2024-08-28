using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddProfileTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6521),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(673));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6043),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(326));

        migrationBuilder.CreateTable(
            name: "WorkProfile",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Certificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExperienceYear = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkProfile", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkProfile_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_WorkProfile_UserId",
            table: "WorkProfile",
            column: "UserId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "WorkProfile");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(673),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6521));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 23, 15, 56, 52, 784, DateTimeKind.Local).AddTicks(326),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 25, 11, 15, 20, 950, DateTimeKind.Local).AddTicks(6043));
    }
}
