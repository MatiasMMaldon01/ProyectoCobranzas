using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehavior0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_Persona_Alumno_AlumnoId",
                table: "Cuota");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Cuota_CuotaId",
                table: "Pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioCuota_Carrera_CarreraId",
                table: "PrecioCuota");

            migrationBuilder.DropForeignKey(
                name: "FK_Proceso_Usuario_UsuarioId",
                table: "Proceso");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_Persona_Alumno_AlumnoId",
                table: "Cuota",
                column: "AlumnoId",
                principalTable: "Persona_Alumno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Cuota_CuotaId",
                table: "Pago",
                column: "CuotaId",
                principalTable: "Cuota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioCuota_Carrera_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proceso_Usuario_UsuarioId",
                table: "Proceso",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_Persona_Alumno_AlumnoId",
                table: "Cuota");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Cuota_CuotaId",
                table: "Pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioCuota_Carrera_CarreraId",
                table: "PrecioCuota");

            migrationBuilder.DropForeignKey(
                name: "FK_Proceso_Usuario_UsuarioId",
                table: "Proceso");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_Persona_Alumno_AlumnoId",
                table: "Cuota",
                column: "AlumnoId",
                principalTable: "Persona_Alumno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Cuota_CuotaId",
                table: "Pago",
                column: "CuotaId",
                principalTable: "Cuota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioCuota_Carrera_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proceso_Usuario_UsuarioId",
                table: "Proceso",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
