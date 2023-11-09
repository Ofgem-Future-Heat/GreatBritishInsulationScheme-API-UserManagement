using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameExternalUserUniqueUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalUsers_ProviderUserId",
                table: "ExternalUsers");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "ExternalUsers",
                newName: "UniqueUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_UniqueUserId",
                table: "ExternalUsers",
                column: "UniqueUserId",
                unique: true,
                filter: "[UniqueUserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalUsers_UniqueUserId",
                table: "ExternalUsers");

            migrationBuilder.RenameColumn(
                name: "UniqueUserId",
                table: "ExternalUsers",
                newName: "ProviderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_ProviderUserId",
                table: "ExternalUsers",
                column: "ProviderUserId",
                unique: true,
                filter: "[ProviderUserId] IS NOT NULL");
        }
    }
}
