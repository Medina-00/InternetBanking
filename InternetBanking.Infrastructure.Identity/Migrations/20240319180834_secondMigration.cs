using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontoInicial",
                schema: "Identity",
                table: "Users",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MontoInicial",
                schema: "Identity",
                table: "Users");
        }
    }
}
