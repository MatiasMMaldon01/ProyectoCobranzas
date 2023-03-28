using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDbStructure010002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Fecha",
                value: new DateTime(2023, 3, 17, 21, 5, 20, 331, DateTimeKind.Local).AddTicks(8338));

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Fecha",
                value: new DateTime(2023, 3, 11, 22, 10, 16, 491, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId");
        }
    }
}
