using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserToUserEmailAndLogReportProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Logs",
                newName: "UserEmail");

            migrationBuilder.CreateTable(
                name: "LogsReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TimeDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsReport");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Logs",
                newName: "User");
        }
    }
}
