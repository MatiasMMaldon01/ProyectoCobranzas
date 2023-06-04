using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadCuotas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persona_Alumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PorcBeca = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona_Alumno", x => x.Id);
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
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoCarrera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    PorcAbonado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoCuota = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    PrecioCuotaId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuota_PrecioCuota_PrecioCuotaId",
                        column: x => x.PrecioCuotaId,
                        principalTable: "PrecioCuota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proceso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntidadMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuotaId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "Apellido", "Direccion", "Dni", "EstaEliminado", "Mail", "Nombre", "Telefono" },
                values: new object[] { 1, "admin", "Rivadavia 1050", "99999999", false, "admin@gmail.com", "Usuario", "9999999" });

            migrationBuilder.InsertData(
                table: "Persona_Empleado",
                columns: new[] { "Id", "AreaTrabajo" },
                values: new object[] { 1, "Cobranzas" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "EstaEliminado", "Fecha", "Nombre", "Password", "PersonaId", "Rol" },
                values: new object[] { 1, false, new DateTime(2023, 5, 21, 15, 14, 6, 251, DateTimeKind.Local).AddTicks(2303), "Admin", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCarrera_AlumnoId",
                table: "AlumnoCarrera",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCarrera_CarreraId",
                table: "AlumnoCarrera",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_AlumnoId",
                table: "Cuota",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_PrecioCuotaId",
                table: "Cuota",
                column: "PrecioCuotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_CuotaId",
                table: "Pago",
                column: "CuotaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCuota_CarreraId",
                table: "PrecioCuota",
                column: "CarreraId",
                unique: true);

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
                name: "AlumnoCarrera");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Persona_Empleado");

            migrationBuilder.DropTable(
                name: "Proceso");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Persona_Alumno");

            migrationBuilder.DropTable(
                name: "PrecioCuota");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Carrera");
        }
    }
}
