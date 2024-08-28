using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AddBlogEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 8, 17, 30, 51, 45, DateTimeKind.Local).AddTicks(595),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7510));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 8, 17, 30, 51, 45, DateTimeKind.Local).AddTicks(318),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7186));

        migrationBuilder.CreateTable(
            name: "Blog",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                View = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Blog", x => x.Id);
                table.ForeignKey(
                    name: "FK_Blog_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Topic",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Topic", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "BlogComment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                BlogId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
            name: "BlogPhoto",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BlogId = table.Column<int>(type: "int", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ONum = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogPhoto", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogPhoto_Blog_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blog",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BlogRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                BlogId = table.Column<int>(type: "int", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogRate_Blog_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blog",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogRate_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BlogTopic",
            columns: table => new
            {
                BlogId = table.Column<int>(type: "int", nullable: false),
                TopicId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogTopic", x => new { x.BlogId, x.TopicId });
                table.ForeignKey(
                    name: "FK_BlogTopic_Blog_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blog",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogTopic_Topic_TopicId",
                    column: x => x.TopicId,
                    principalTable: "Topic",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BlogCommentLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                UserId = table.Column<int>(type: "int", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                ReplyParentId = table.Column<int>(type: "int", nullable: true),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ReplyId = table.Column<int>(type: "int", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                UserId = table.Column<int>(type: "int", nullable: false),
                ReplyId = table.Column<int>(type: "int", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
            name: "IX_Blog_UserId",
            table: "Blog",
            column: "UserId");

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
            name: "IX_BlogPhoto_BlogId",
            table: "BlogPhoto",
            column: "BlogId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogRate_BlogId",
            table: "BlogRate",
            column: "BlogId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogRate_UserId",
            table: "BlogRate",
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

        migrationBuilder.CreateIndex(
            name: "IX_BlogTopic_TopicId",
            table: "BlogTopic",
            column: "TopicId");

        migrationBuilder.CreateIndex(
            name: "IX_Topic_Name",
            table: "Topic",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BlogCommentLike");

        migrationBuilder.DropTable(
            name: "BlogPhoto");

        migrationBuilder.DropTable(
            name: "BlogRate");

        migrationBuilder.DropTable(
            name: "BlogReplyLike");

        migrationBuilder.DropTable(
            name: "BlogTopic");

        migrationBuilder.DropTable(
            name: "BlogReply");

        migrationBuilder.DropTable(
            name: "Topic");

        migrationBuilder.DropTable(
            name: "BlogComment");

        migrationBuilder.DropTable(
            name: "Blog");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7510),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 8, 17, 30, 51, 45, DateTimeKind.Local).AddTicks(595));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 5, 26, 23, 55, 14, 85, DateTimeKind.Local).AddTicks(7186),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 8, 17, 30, 51, 45, DateTimeKind.Local).AddTicks(318));
    }
}
