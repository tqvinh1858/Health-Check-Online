using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_OnDeleteBehavior_Blog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Blog_User_UserId",
            table: "Blog");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogPhoto_Blog_BlogId",
            table: "BlogPhoto");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogRate_Blog_BlogId",
            table: "BlogRate");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogRate_User_UserId",
            table: "BlogRate");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogTopic_Blog_BlogId",
            table: "BlogTopic");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 12, 21, 42, 29, 263, DateTimeKind.Local).AddTicks(8108),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 11, 12, 5, 25, 472, DateTimeKind.Local).AddTicks(881));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 12, 21, 42, 29, 263, DateTimeKind.Local).AddTicks(7717),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 11, 12, 5, 25, 471, DateTimeKind.Local).AddTicks(9105));

        migrationBuilder.AddForeignKey(
            name: "FK_Blog_User_UserId",
            table: "Blog",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BlogPhoto_Blog_BlogId",
            table: "BlogPhoto",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BlogRate_Blog_BlogId",
            table: "BlogRate",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BlogRate_User_UserId",
            table: "BlogRate",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BlogTopic_Blog_BlogId",
            table: "BlogTopic",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Blog_User_UserId",
            table: "Blog");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogPhoto_Blog_BlogId",
            table: "BlogPhoto");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogRate_Blog_BlogId",
            table: "BlogRate");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogRate_User_UserId",
            table: "BlogRate");

        migrationBuilder.DropForeignKey(
            name: "FK_BlogTopic_Blog_BlogId",
            table: "BlogTopic");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 11, 12, 5, 25, 472, DateTimeKind.Local).AddTicks(881),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 12, 21, 42, 29, 263, DateTimeKind.Local).AddTicks(8108));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 11, 12, 5, 25, 471, DateTimeKind.Local).AddTicks(9105),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 12, 21, 42, 29, 263, DateTimeKind.Local).AddTicks(7717));

        migrationBuilder.AddForeignKey(
            name: "FK_Blog_User_UserId",
            table: "Blog",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BlogPhoto_Blog_BlogId",
            table: "BlogPhoto",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BlogRate_Blog_BlogId",
            table: "BlogRate",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BlogRate_User_UserId",
            table: "BlogRate",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BlogTopic_Blog_BlogId",
            table: "BlogTopic",
            column: "BlogId",
            principalTable: "Blog",
            principalColumn: "Id");
    }
}
