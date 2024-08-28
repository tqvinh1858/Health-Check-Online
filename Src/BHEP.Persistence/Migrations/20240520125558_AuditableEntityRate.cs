using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHEP.Persistence.Migrations;

/// <inheritdoc />
public partial class AuditableEntityRate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Appointment");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "UserRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "UserRate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "UserRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "ServiceRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "ServiceRate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "ServiceRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "ProductRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "ProductRate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "ProductRate",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "UserRate");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "UserRate");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "UserRate");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "ServiceRate");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "ServiceRate");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "ServiceRate");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "ProductRate");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "ProductRate");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "ProductRate");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "User",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Appointment",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }
}
