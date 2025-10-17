using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Migrations
{
    /// <inheritdoc />
    public partial class IndexToInactiveAndRegisterDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_REGISTERDATE",
                table: "Clients",
                column: "RegisterDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENT_ISINACTIVE",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_CLIENT_REGISTERDATE",
                table: "Clients");
        }
    }
}
