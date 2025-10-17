using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Migrations
{
    /// <inheritdoc />
    public partial class Fix_InactivesClientIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients",
                column: "IsActive",
                filter: "[IsActive] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients",
                column: "IsActive");
        }
    }
}
