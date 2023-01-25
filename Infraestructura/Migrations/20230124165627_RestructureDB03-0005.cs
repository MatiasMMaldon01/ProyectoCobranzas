using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class RestructureDB030005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrecioCuotaId",
                table: "Cuota",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_PrecioCuotaId",
                table: "Cuota",
                column: "PrecioCuotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_PrecioCuota_PrecioCuotaId",
                table: "Cuota",
                column: "PrecioCuotaId",
                principalTable: "PrecioCuota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_PrecioCuota_PrecioCuotaId",
                table: "Cuota");

            migrationBuilder.DropIndex(
                name: "IX_Cuota_PrecioCuotaId",
                table: "Cuota");

            migrationBuilder.DropColumn(
                name: "PrecioCuotaId",
                table: "Cuota");
        }
    }
}
