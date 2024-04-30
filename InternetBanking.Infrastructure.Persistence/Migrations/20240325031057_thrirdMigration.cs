using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class thrirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuentaAhorroIdCuentaAhorro",
                table: "Transferencias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_CuentaAhorroIdCuentaAhorro",
                table: "Transferencias",
                column: "CuentaAhorroIdCuentaAhorro");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencias_CuentasAhorro_CuentaAhorroIdCuentaAhorro",
                table: "Transferencias",
                column: "CuentaAhorroIdCuentaAhorro",
                principalTable: "CuentasAhorro",
                principalColumn: "IdCuentaAhorro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferencias_CuentasAhorro_CuentaAhorroIdCuentaAhorro",
                table: "Transferencias");

            migrationBuilder.DropIndex(
                name: "IX_Transferencias_CuentaAhorroIdCuentaAhorro",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "CuentaAhorroIdCuentaAhorro",
                table: "Transferencias");
        }
    }
}
