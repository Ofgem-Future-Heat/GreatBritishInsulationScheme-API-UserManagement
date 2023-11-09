using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierLicenceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierLicences",
                table: "SupplierLicences");

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNo",
                table: "SupplierLicences",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "LicenceName",
                table: "SupplierLicences",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierLicenceReference",
                table: "SupplierLicences",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "SupplierLicenceId",
                table: "SupplierLicences",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierLicences",
                table: "SupplierLicences",
                column: "SupplierLicenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierLicences",
                table: "SupplierLicences");

            migrationBuilder.DropColumn(
                name: "SupplierLicenceId",
                table: "SupplierLicences");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierLicenceReference",
                table: "SupplierLicences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNo",
                table: "SupplierLicences",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LicenceName",
                table: "SupplierLicences",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierLicences",
                table: "SupplierLicences",
                column: "SupplierLicenceReference");
        }
    }
}
