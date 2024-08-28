using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_Flow_CoinTransactionPaymentEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PaymentVoucher_Payment_PaymentId",
            table: "PaymentVoucher");

        migrationBuilder.DropForeignKey(
            name: "FK_ProductRate_Payment_PaymentId",
            table: "ProductRate");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceRate_Payment_PaymentId",
            table: "ServiceRate");

        migrationBuilder.DropTable(
            name: "OrderProductDetail");

        migrationBuilder.DropTable(
            name: "OrderServiceDetail");

        migrationBuilder.DropTable(
            name: "Order");

        migrationBuilder.RenameColumn(
            name: "PaymentId",
            table: "ServiceRate",
            newName: "TransactionId");

        migrationBuilder.RenameIndex(
            name: "IX_ServiceRate_PaymentId",
            table: "ServiceRate",
            newName: "IX_ServiceRate_TransactionId");

        migrationBuilder.RenameColumn(
            name: "PaymentId",
            table: "ProductRate",
            newName: "TransactionId");

        migrationBuilder.RenameIndex(
            name: "IX_ProductRate_PaymentId",
            table: "ProductRate",
            newName: "IX_ProductRate_TransactionId");

        migrationBuilder.RenameColumn(
            name: "PaymentId",
            table: "PaymentVoucher",
            newName: "TransactionId");

        migrationBuilder.RenameColumn(
            name: "CreateDate",
            table: "CoinTransaction",
            newName: "UpdatedDate");

        migrationBuilder.RenameColumn(
            name: "OrderId",
            table: "Code",
            newName: "TransactionId");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 249, DateTimeKind.Local).AddTicks(200),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1700));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 248, DateTimeKind.Local).AddTicks(9831),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1165));

        migrationBuilder.AddColumn<int>(
            name: "CodeId",
            table: "CoinTransaction",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "CoinTransaction",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "CoinTransaction",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "CoinTransaction",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsMinus",
            table: "CoinTransaction",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "VoucherId",
            table: "CoinTransaction",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "BlogRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "BlogRate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "BlogRate",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "BlogRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.CreateTable(
            name: "Post",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                SpecialistId = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Age = table.Column<int>(type: "int", nullable: false),
                Gender = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Post", x => x.Id);
                table.ForeignKey(
                    name: "FK_Post_Specialist_UserId",
                    column: x => x.UserId,
                    principalTable: "Specialist",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Post_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ProductTransaction",
            columns: table => new
            {
                ProductId = table.Column<int>(type: "int", nullable: false),
                TransactionId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTransaction", x => new { x.TransactionId, x.ProductId });
                table.ForeignKey(
                    name: "FK_ProductTransaction_CoinTransaction_TransactionId",
                    column: x => x.TransactionId,
                    principalTable: "CoinTransaction",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProductTransaction_Product_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Product",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ServiceTransaction",
            columns: table => new
            {
                ServiceId = table.Column<int>(type: "int", nullable: false),
                TransactionId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ServiceTransaction", x => new { x.TransactionId, x.ServiceId });
                table.ForeignKey(
                    name: "FK_ServiceTransaction_CoinTransaction_TransactionId",
                    column: x => x.TransactionId,
                    principalTable: "CoinTransaction",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ServiceTransaction_Service_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Service",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Comment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                PostId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comment_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Comment_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PostLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                PostId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PostLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_PostLike_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PostLike_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CommentLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CommentLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_CommentLike_Comment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "Comment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CommentLike_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Reply",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                ReplyParentId = table.Column<int>(type: "int", nullable: true),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reply", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reply_Comment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "Comment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Reply_Reply_ReplyParentId",
                    column: x => x.ReplyParentId,
                    principalTable: "Reply",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Reply_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ReplyLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ReplyId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReplyLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_ReplyLike_Reply_ReplyId",
                    column: x => x.ReplyId,
                    principalTable: "Reply",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ReplyLike_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            unique: true,
            filter: "[CodeId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_PostId",
            table: "Comment",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_UserId",
            table: "Comment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_CommentLike_CommentId",
            table: "CommentLike",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_CommentLike_UserId",
            table: "CommentLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Post_UserId",
            table: "Post",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PostLike_PostId",
            table: "PostLike",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_PostLike_UserId",
            table: "PostLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTransaction_ProductId",
            table: "ProductTransaction",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_Reply_CommentId",
            table: "Reply",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_Reply_ReplyParentId",
            table: "Reply",
            column: "ReplyParentId");

        migrationBuilder.CreateIndex(
            name: "IX_Reply_UserId",
            table: "Reply",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ReplyLike_ReplyId",
            table: "ReplyLike",
            column: "ReplyId");

        migrationBuilder.CreateIndex(
            name: "IX_ReplyLike_UserId",
            table: "ReplyLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceTransaction_ServiceId",
            table: "ServiceTransaction",
            column: "ServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction",
            column: "CodeId",
            principalTable: "Code",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PaymentVoucher_CoinTransaction_TransactionId",
            table: "PaymentVoucher",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ProductRate_CoinTransaction_TransactionId",
            table: "ProductRate",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceRate_CoinTransaction_TransactionId",
            table: "ServiceRate",
            column: "TransactionId",
            principalTable: "CoinTransaction",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CoinTransaction_Code_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropForeignKey(
            name: "FK_PaymentVoucher_CoinTransaction_TransactionId",
            table: "PaymentVoucher");

        migrationBuilder.DropForeignKey(
            name: "FK_ProductRate_CoinTransaction_TransactionId",
            table: "ProductRate");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceRate_CoinTransaction_TransactionId",
            table: "ServiceRate");

        migrationBuilder.DropTable(
            name: "CommentLike");

        migrationBuilder.DropTable(
            name: "PostLike");

        migrationBuilder.DropTable(
            name: "ProductTransaction");

        migrationBuilder.DropTable(
            name: "ReplyLike");

        migrationBuilder.DropTable(
            name: "ServiceTransaction");

        migrationBuilder.DropTable(
            name: "Reply");

        migrationBuilder.DropTable(
            name: "Comment");

        migrationBuilder.DropTable(
            name: "Post");

        migrationBuilder.DropIndex(
            name: "IX_CoinTransaction_CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "CodeId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "IsMinus",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "VoucherId",
            table: "CoinTransaction");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "BlogRate");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "BlogRate");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "BlogRate");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "BlogRate");

        migrationBuilder.RenameColumn(
            name: "TransactionId",
            table: "ServiceRate",
            newName: "PaymentId");

        migrationBuilder.RenameIndex(
            name: "IX_ServiceRate_TransactionId",
            table: "ServiceRate",
            newName: "IX_ServiceRate_PaymentId");

        migrationBuilder.RenameColumn(
            name: "TransactionId",
            table: "ProductRate",
            newName: "PaymentId");

        migrationBuilder.RenameIndex(
            name: "IX_ProductRate_TransactionId",
            table: "ProductRate",
            newName: "IX_ProductRate_PaymentId");

        migrationBuilder.RenameColumn(
            name: "TransactionId",
            table: "PaymentVoucher",
            newName: "PaymentId");

        migrationBuilder.RenameColumn(
            name: "UpdatedDate",
            table: "CoinTransaction",
            newName: "CreateDate");

        migrationBuilder.RenameColumn(
            name: "TransactionId",
            table: "Code",
            newName: "OrderId");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1700),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 249, DateTimeKind.Local).AddTicks(200));

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Payment",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(2024, 6, 13, 17, 13, 33, 389, DateTimeKind.Local).AddTicks(1165),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValue: new DateTime(2024, 6, 13, 21, 12, 6, 248, DateTimeKind.Local).AddTicks(9831));

        migrationBuilder.CreateTable(
            name: "Order",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CodeId = table.Column<int>(type: "int", nullable: true),
                PaymentId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false),
                OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                TotalPrice = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Order", x => x.Id);
                table.ForeignKey(
                    name: "FK_Order_Code_CodeId",
                    column: x => x.CodeId,
                    principalTable: "Code",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Order_Payment_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Order_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "OrderProductDetail",
            columns: table => new
            {
                OrderId = table.Column<int>(type: "int", nullable: false),
                ProductId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderProductDetail", x => new { x.OrderId, x.ProductId });
                table.ForeignKey(
                    name: "FK_OrderProductDetail_Order_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Order",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_OrderProductDetail_Product_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Product",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "OrderServiceDetail",
            columns: table => new
            {
                OrderId = table.Column<int>(type: "int", nullable: false),
                ServiceId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderServiceDetail", x => new { x.OrderId, x.ServiceId });
                table.ForeignKey(
                    name: "FK_OrderServiceDetail_Order_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Order",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_OrderServiceDetail_Service_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Service",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Order_CodeId",
            table: "Order",
            column: "CodeId",
            unique: true,
            filter: "[CodeId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Order_PaymentId",
            table: "Order",
            column: "PaymentId");

        migrationBuilder.CreateIndex(
            name: "IX_Order_UserId",
            table: "Order",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderProductDetail_ProductId",
            table: "OrderProductDetail",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderServiceDetail_ServiceId",
            table: "OrderServiceDetail",
            column: "ServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_PaymentVoucher_Payment_PaymentId",
            table: "PaymentVoucher",
            column: "PaymentId",
            principalTable: "Payment",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ProductRate_Payment_PaymentId",
            table: "ProductRate",
            column: "PaymentId",
            principalTable: "Payment",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceRate_Payment_PaymentId",
            table: "ServiceRate",
            column: "PaymentId",
            principalTable: "Payment",
            principalColumn: "Id");
    }
}
