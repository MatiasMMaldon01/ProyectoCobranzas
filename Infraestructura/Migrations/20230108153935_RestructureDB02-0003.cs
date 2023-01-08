using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class RestructureDB020003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Cuota",
                newName: "MontoCuota");

            migrationBuilder.AddColumn<decimal>(
                name: "MontoAbonado",
                table: "Cuota",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoAbonado",
                table: "Cuota");

            migrationBuilder.RenameColumn(
                name: "MontoCuota",
                table: "Cuota",
                newName: "Monto");
        }
    }
}
