using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierLicenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierLicences",
                columns: table => new
                {
                    SupplierLicenceReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierLicences", x => x.SupplierLicenceReference);
                    table.ForeignKey(
                        name: "FK_SupplierLicences_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierLicences_SupplierId",
                table: "SupplierLicences",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierLicences_SupplierLicenceReference",
                table: "SupplierLicences",
                column: "SupplierLicenceReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierLicences");
        }
    }
}
