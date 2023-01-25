using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class RestructureDB040006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoCuota",
                table: "Cuota");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Cuota",
                newName: "EstadoCuota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstadoCuota",
                table: "Cuota",
                newName: "Rol");

            migrationBuilder.AddColumn<decimal>(
                name: "MontoCuota",
                table: "Cuota",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
