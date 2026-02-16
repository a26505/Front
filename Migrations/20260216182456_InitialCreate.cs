using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REPS_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ejercicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionTecnica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenMusculosUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrupoMuscular = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false),
                    IconoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreRutinaSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaFinalizada = table.Column<bool>(type: "bit", nullable: false),
                    DuracionRealMinutos = table.Column<int>(type: "int", nullable: false),
                    PuntosGanados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanActual = table.Column<int>(type: "int", nullable: false),
                    FechaFinSuscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PuntosTotales = table.Column<int>(type: "int", nullable: false),
                    PuntosLogros = table.Column<int>(type: "int", nullable: false),
                    PuntosRecords = table.Column<int>(type: "int", nullable: false),
                    RachaDias = table.Column<int>(type: "int", nullable: false),
                    FechaUltimaActividad = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoAmigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    EstaBorrado = table.Column<bool>(type: "bit", nullable: false),
                    RangoGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesMusculares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    NombreMusculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionImpacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesMusculares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesMusculares_Ejercicios_EjercicioId",
                        column: x => x.EjercicioId,
                        principalTable: "Ejercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsPersonales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    PesoMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRecord = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PesoAnterior = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsPersonales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordsPersonales_Ejercicios_EjercicioId",
                        column: x => x.EjercicioId,
                        principalTable: "Ejercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    LogroId = table.Column<int>(type: "int", nullable: false),
                    Progreso = table.Column<double>(type: "float", nullable: false),
                    Desbloqueado = table.Column<bool>(type: "bit", nullable: false),
                    FechaObtencion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioLogros_Logros_LogroId",
                        column: x => x.LogroId,
                        principalTable: "Logros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amistades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitanteId = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false),
                    FechaAmistad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aceptada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amistades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amistades_Usuarios_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amistades_Usuarios_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false),
                    EsPublica = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EsGeneradaPorIA = table.Column<bool>(type: "bit", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Descargas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rutinas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entrenamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RutinaId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrenamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrenamientos_Rutinas_RutinaId",
                        column: x => x.RutinaId,
                        principalTable: "Rutinas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entrenamientos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RutinaEjercicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RutinaId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescansoSegundos = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    PorcentajeDelPeso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PesoSugerido = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutinaEjercicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RutinaEjercicios_Ejercicios_EjercicioId",
                        column: x => x.EjercicioId,
                        principalTable: "Ejercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutinaEjercicios_Rutinas_RutinaId",
                        column: x => x.RutinaId,
                        principalTable: "Rutinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerieLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    NumeroSerie = table.Column<int>(type: "int", nullable: false),
                    PesoUsado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepsRealizadas = table.Column<int>(type: "int", nullable: false),
                    Completada = table.Column<bool>(type: "bit", nullable: false),
                    EntrenamientoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieLogs_Ejercicios_EjercicioId",
                        column: x => x.EjercicioId,
                        principalTable: "Ejercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerieLogs_Entrenamientos_EntrenamientoId",
                        column: x => x.EntrenamientoId,
                        principalTable: "Entrenamientos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SerieLogs_Sesiones_SesionId",
                        column: x => x.SesionId,
                        principalTable: "Sesiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amistades_ReceptorId",
                table: "Amistades",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Amistades_SolicitanteId",
                table: "Amistades",
                column: "SolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMusculares_EjercicioId",
                table: "DetallesMusculares",
                column: "EjercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrenamientos_RutinaId",
                table: "Entrenamientos",
                column: "RutinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrenamientos_UsuarioId",
                table: "Entrenamientos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsPersonales_EjercicioId",
                table: "RecordsPersonales",
                column: "EjercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RutinaEjercicios_EjercicioId",
                table: "RutinaEjercicios",
                column: "EjercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RutinaEjercicios_RutinaId",
                table: "RutinaEjercicios",
                column: "RutinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutinas_UsuarioId",
                table: "Rutinas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieLogs_EjercicioId",
                table: "SerieLogs",
                column: "EjercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieLogs_EntrenamientoId",
                table: "SerieLogs",
                column: "EntrenamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieLogs_SesionId",
                table: "SerieLogs",
                column: "SesionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogros_LogroId",
                table: "UsuarioLogros",
                column: "LogroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amistades");

            migrationBuilder.DropTable(
                name: "DetallesMusculares");

            migrationBuilder.DropTable(
                name: "RecordsPersonales");

            migrationBuilder.DropTable(
                name: "RutinaEjercicios");

            migrationBuilder.DropTable(
                name: "SerieLogs");

            migrationBuilder.DropTable(
                name: "UsuarioLogros");

            migrationBuilder.DropTable(
                name: "Ejercicios");

            migrationBuilder.DropTable(
                name: "Entrenamientos");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "Logros");

            migrationBuilder.DropTable(
                name: "Rutinas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
