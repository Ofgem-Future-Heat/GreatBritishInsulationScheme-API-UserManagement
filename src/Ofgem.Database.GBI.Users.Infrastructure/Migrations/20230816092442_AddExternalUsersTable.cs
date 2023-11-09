using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_EmailAddress",
                table: "ExternalUsers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_ProviderUserId",
                table: "ExternalUsers",
                column: "ProviderUserId",
                unique: true,
                filter: "[ProviderUserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalUsers");
        }
    }
}
