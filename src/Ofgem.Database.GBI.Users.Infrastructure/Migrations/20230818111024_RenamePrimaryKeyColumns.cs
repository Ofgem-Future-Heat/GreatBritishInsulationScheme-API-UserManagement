using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePrimaryKeyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "SupplierName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Suppliers",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                newName: "IX_Suppliers_SupplierName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ExternalUsers",
                newName: "ExternalUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Suppliers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_SupplierName",
                table: "Suppliers",
                newName: "IX_Suppliers_Name");

            migrationBuilder.RenameColumn(
                name: "ExternalUserId",
                table: "ExternalUsers",
                newName: "Id");
        }
    }
}
