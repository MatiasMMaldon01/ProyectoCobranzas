using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class MapeoInicial0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadCuotas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrecioCuota",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarreraId = table.Column<long>(type: "bigint", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecioCuota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecioCuota_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona_Alumno",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Legajo = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarreraId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona_Alumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Alumno_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_Alumno_Persona_Id",
                        column: x => x.Id,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona_Empleado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AreaTrabajo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Empleado_Persona_Id",
                        column: x => x.Id,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<long>(type: "bigint", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuota_Persona_Alumno_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Persona_Alumno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proceso",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntidadMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proceso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proceso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuotaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Cuota_CuotaId",
                        column: x => x.CuotaId,
                        principalTable: "Cuota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_AlumnoId",
                table: "Cuota",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_CuotaId",
                table: "Pago",
                column: "CuotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_Alumno_CarreraId",
                table: "Persona_Alumno",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Proceso_UsuarioId",
                table: "Proceso",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PersonaId",
                table: "Usuario",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Persona_Empleado");

            migrationBuilder.DropTable(
                name: "PrecioCuota");

            migrationBuilder.DropTable(
                name: "Proceso");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Persona_Alumno");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
