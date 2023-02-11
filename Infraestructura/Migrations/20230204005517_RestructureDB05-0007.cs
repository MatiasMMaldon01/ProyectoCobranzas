using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class RestructureDB050007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno");

            migrationBuilder.DropIndex(
                name: "IX_Persona_Alumno_CarreraId",
                table: "Persona_Alumno");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Persona_Alumno");

            migrationBuilder.CreateTable(
                name: "AlumnoCarrera",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<long>(type: "bigint", nullable: false),
                    CarreraId = table.Column<long>(type: "bigint", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoCarrera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlumnoCarrera_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlumnoCarrera_Persona_Alumno_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Persona_Alumno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCarrera_AlumnoId",
                table: "AlumnoCarrera",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCarrera_CarreraId",
                table: "AlumnoCarrera",
                column: "CarreraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoCarrera");

            migrationBuilder.AddColumn<long>(
                name: "CarreraId",
                table: "Persona_Alumno",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_Alumno_CarreraId",
                table: "Persona_Alumno",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Alumno_Carrera_CarreraId",
                table: "Persona_Alumno",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
