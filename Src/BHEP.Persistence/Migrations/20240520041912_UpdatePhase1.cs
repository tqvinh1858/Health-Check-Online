using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdatePhase1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "AppointmentId",
            table: "User",
            type: "int",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Payment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                OrderId = table.Column<int>(type: "int", nullable: false),
                Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payment_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Product",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Price = table.Column<float>(type: "real", nullable: false),
                Stock = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Service",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Price = table.Column<float>(type: "real", nullable: false),
                Duration = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Service", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CustomerId = table.Column<int>(type: "int", nullable: false),
                EmployeeId = table.Column<int>(type: "int", nullable: false),
                AppointmentId = table.Column<int>(type: "int", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Rate = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserRate_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserRate_User_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "User",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserRate_User_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Voucher",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Discount = table.Column<float>(type: "real", nullable: false),
                Stock = table.Column<int>(type: "int", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Voucher", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Order",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                PaymentId = table.Column<int>(type: "int", nullable: false),
                OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                TotalPrice = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Order", x => x.Id);
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
            name: "ProductRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ProductId = table.Column<int>(type: "int", nullable: false),
                PaymentId = table.Column<int>(type: "int", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductRate_Payment_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProductRate_Product_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Product",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProductRate_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Code",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ServiceId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Code", x => x.Id);
                table.ForeignKey(
                    name: "FK_Code_Service_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Service",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Duration",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ServiceId = table.Column<int>(type: "int", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Duration", x => x.Id);
                table.ForeignKey(
                    name: "FK_Duration_Service_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Service",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Duration_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ServiceRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ServiceId = table.Column<int>(type: "int", nullable: false),
                PaymentId = table.Column<int>(type: "int", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Rate = table.Column<float>(type: "real", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ServiceRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_ServiceRate_Payment_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ServiceRate_Service_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Service",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ServiceRate_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PaymentVoucher",
            columns: table => new
            {
                PaymentId = table.Column<int>(type: "int", nullable: false),
                VoucherId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PaymentVoucher", x => new { x.PaymentId, x.VoucherId });
                table.ForeignKey(
                    name: "FK_PaymentVoucher_Payment_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payment",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PaymentVoucher_Voucher_VoucherId",
                    column: x => x.VoucherId,
                    principalTable: "Voucher",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "UserVoucher",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false),
                VoucherId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserVoucher", x => new { x.UserId, x.VoucherId });
                table.ForeignKey(
                    name: "FK_UserVoucher_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserVoucher_Voucher_VoucherId",
                    column: x => x.VoucherId,
                    principalTable: "Voucher",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "OrderProductDetail",
            columns: table => new
            {
                ProductId = table.Column<int>(type: "int", nullable: false),
                OrderId = table.Column<int>(type: "int", nullable: false),
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
                ServiceId = table.Column<int>(type: "int", nullable: false),
                OrderId = table.Column<int>(type: "int", nullable: false)
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
            name: "IX_User_AppointmentId",
            table: "User",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Code_ServiceId",
            table: "Code",
            column: "ServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_Duration_ServiceId",
            table: "Duration",
            column: "ServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_Duration_UserId",
            table: "Duration",
            column: "UserId");

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

        migrationBuilder.CreateIndex(
            name: "IX_Payment_UserId",
            table: "Payment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PaymentVoucher_VoucherId",
            table: "PaymentVoucher",
            column: "VoucherId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductRate_PaymentId",
            table: "ProductRate",
            column: "PaymentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductRate_ProductId",
            table: "ProductRate",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductRate_UserId",
            table: "ProductRate",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceRate_PaymentId",
            table: "ServiceRate",
            column: "PaymentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ServiceRate_ServiceId",
            table: "ServiceRate",
            column: "ServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceRate_UserId",
            table: "ServiceRate",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRate_AppointmentId",
            table: "UserRate",
            column: "AppointmentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserRate_CustomerId",
            table: "UserRate",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRate_EmployeeId",
            table: "UserRate",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_UserVoucher_VoucherId",
            table: "UserVoucher",
            column: "VoucherId");

        migrationBuilder.AddForeignKey(
            name: "FK_User_Appointment_AppointmentId",
            table: "User",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_User_Appointment_AppointmentId",
            table: "User");

        migrationBuilder.DropTable(
            name: "Code");

        migrationBuilder.DropTable(
            name: "Duration");

        migrationBuilder.DropTable(
            name: "OrderProductDetail");

        migrationBuilder.DropTable(
            name: "OrderServiceDetail");

        migrationBuilder.DropTable(
            name: "PaymentVoucher");

        migrationBuilder.DropTable(
            name: "ProductRate");

        migrationBuilder.DropTable(
            name: "ServiceRate");

        migrationBuilder.DropTable(
            name: "UserRate");

        migrationBuilder.DropTable(
            name: "UserVoucher");

        migrationBuilder.DropTable(
            name: "Order");

        migrationBuilder.DropTable(
            name: "Product");

        migrationBuilder.DropTable(
            name: "Service");

        migrationBuilder.DropTable(
            name: "Voucher");

        migrationBuilder.DropTable(
            name: "Payment");

        migrationBuilder.DropIndex(
            name: "IX_User_AppointmentId",
            table: "User");

        migrationBuilder.DropColumn(
            name: "AppointmentId",
            table: "User");
    }
}
