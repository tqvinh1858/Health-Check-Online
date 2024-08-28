using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Delete_CommentReply_Blog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BlogCommentLike");

        migrationBuilder.DropTable(
            name: "BlogReplyLike");

        migrationBuilder.DropTable(
            name: "BlogReply");

        migrationBuilder.DropTable(
            name: "BlogComment");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1700),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4434));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1165),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4019));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4434),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1700));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 7, 48, 6, 798, DateTimeKind.Local).AddTicks(4019),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1165));

        migrationBuilder.CreateTable(
            name: "BlogComment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BlogId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogComment", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogComment_Blog_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blog",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BlogComment_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BlogCommentLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CommentId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogCommentLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogCommentLike_BlogComment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "BlogComment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogCommentLike_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BlogReply",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CommentId = table.Column<int>(type: "int", nullable: false),
                ReplyId = table.Column<int>(type: "int", nullable: true),
                UserId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                ReplyParentId = table.Column<int>(type: "int", nullable: true),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogReply", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogReply_BlogComment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "BlogComment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogReply_BlogReply_ReplyId",
                    column: x => x.ReplyId,
                    principalTable: "BlogReply",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogReply_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BlogReplyLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReplyId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogReplyLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogReplyLike_BlogReply_ReplyId",
                    column: x => x.ReplyId,
                    principalTable: "BlogReply",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogReplyLike_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_BlogComment_BlogId",
            table: "BlogComment",
            column: "BlogId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogComment_UserId",
            table: "BlogComment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogCommentLike_CommentId",
            table: "BlogCommentLike",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogCommentLike_UserId",
            table: "BlogCommentLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogReply_CommentId",
            table: "BlogReply",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogReply_ReplyId",
            table: "BlogReply",
            column: "ReplyId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogReply_UserId",
            table: "BlogReply",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogReplyLike_ReplyId",
            table: "BlogReplyLike",
            column: "ReplyId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogReplyLike_UserId",
            table: "BlogReplyLike",
            column: "UserId");
    }
}
