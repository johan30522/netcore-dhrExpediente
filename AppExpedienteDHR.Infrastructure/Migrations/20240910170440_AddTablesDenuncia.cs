using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesDenuncia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dhr");

            migrationBuilder.CreateTable(
                name: "Denunciante",
                schema: "dhr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacionId = table.Column<int>(type: "int", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SexoId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    PaisOrigenCodigo = table.Column<int>(type: "int", nullable: false),
                    EscolaridadId = table.Column<int>(type: "int", nullable: false),
                    TelefonoCelular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EsMenorEdad = table.Column<bool>(type: "bit", nullable: false),
                    TieneRequerimientoEspecial = table.Column<bool>(type: "bit", nullable: false),
                    ProvinciaCodigo = table.Column<int>(type: "int", nullable: false),
                    CantonCodigo = table.Column<int>(type: "int", nullable: false),
                    DistritoCodigo = table.Column<int>(type: "int", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denunciante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denunciante_Cantones_CantonCodigo",
                        column: x => x.CantonCodigo,
                        principalSchema: "gen",
                        principalTable: "Cantones",
                        principalColumn: "CodigoCanton",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_Distritos_DistritoCodigo",
                        column: x => x.DistritoCodigo,
                        principalSchema: "gen",
                        principalTable: "Distritos",
                        principalColumn: "CodigoDistrito",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_Escolaridad_EscolaridadId",
                        column: x => x.EscolaridadId,
                        principalSchema: "gen",
                        principalTable: "Escolaridad",
                        principalColumn: "EscolaridadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_EstadoCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalSchema: "gen",
                        principalTable: "EstadoCivil",
                        principalColumn: "EstadoCivilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_Paises_PaisOrigenCodigo",
                        column: x => x.PaisOrigenCodigo,
                        principalSchema: "gen",
                        principalTable: "Paises",
                        principalColumn: "CodigoNumerico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_Provincias_ProvinciaCodigo",
                        column: x => x.ProvinciaCodigo,
                        principalSchema: "gen",
                        principalTable: "Provincias",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalSchema: "gen",
                        principalTable: "Sexo",
                        principalColumn: "SexoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denunciante_TipoIdentificacion_TipoIdentificacionId",
                        column: x => x.TipoIdentificacionId,
                        principalSchema: "gen",
                        principalTable: "TipoIdentificacion",
                        principalColumn: "TipoIdentificacionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaAfectada",
                schema: "dhr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacionId = table.Column<int>(type: "int", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SexoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaAfectada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonaAfectada_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalSchema: "gen",
                        principalTable: "Sexo",
                        principalColumn: "SexoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaAfectada_TipoIdentificacion_TipoIdentificacionId",
                        column: x => x.TipoIdentificacionId,
                        principalSchema: "gen",
                        principalTable: "TipoIdentificacion",
                        principalColumn: "TipoIdentificacionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Denuncia",
                schema: "dhr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DenuncianteId = table.Column<int>(type: "int", nullable: false),
                    DetalleDenuncia = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Petitoria = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FechaDenuncia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonaAfectadaId = table.Column<int>(type: "int", nullable: true),
                    AceptaTerminos = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denuncia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denuncia_Denunciante_DenuncianteId",
                        column: x => x.DenuncianteId,
                        principalSchema: "dhr",
                        principalTable: "Denunciante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Denuncia_PersonaAfectada_PersonaAfectadaId",
                        column: x => x.PersonaAfectadaId,
                        principalSchema: "dhr",
                        principalTable: "PersonaAfectada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DenunciaAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DenunciaId = table.Column<int>(type: "int", nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DenunciaAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DenunciaAdjuntos_Denuncia_DenunciaId",
                        column: x => x.DenunciaId,
                        principalSchema: "dhr",
                        principalTable: "Denuncia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expedientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DenunciaId = table.Column<int>(type: "int", nullable: false),
                    DenuncianteId = table.Column<int>(type: "int", nullable: false),
                    EstadoActual = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expedientes_Denuncia_DenunciaId",
                        column: x => x.DenunciaId,
                        principalSchema: "dhr",
                        principalTable: "Denuncia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expedientes_Denunciante_DenuncianteId",
                        column: x => x.DenuncianteId,
                        principalSchema: "dhr",
                        principalTable: "Denunciante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Denuncia_DenuncianteId",
                schema: "dhr",
                table: "Denuncia",
                column: "DenuncianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Denuncia_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncia",
                column: "PersonaAfectadaId");

            migrationBuilder.CreateIndex(
                name: "IX_DenunciaAdjuntos_DenunciaId",
                table: "DenunciaAdjuntos",
                column: "DenunciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_CantonCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "CantonCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_DistritoCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "DistritoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_EscolaridadId",
                schema: "dhr",
                table: "Denunciante",
                column: "EscolaridadId");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_EstadoCivilId",
                schema: "dhr",
                table: "Denunciante",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "PaisOrigenCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "ProvinciaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_SexoId",
                schema: "dhr",
                table: "Denunciante",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Denunciante_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciante",
                column: "TipoIdentificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DenunciaId",
                table: "Expedientes",
                column: "DenunciaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DenuncianteId",
                table: "Expedientes",
                column: "DenuncianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaAfectada_SexoId",
                schema: "dhr",
                table: "PersonaAfectada",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaAfectada_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonaAfectada",
                column: "TipoIdentificacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DenunciaAdjuntos");

            migrationBuilder.DropTable(
                name: "Expedientes");

            migrationBuilder.DropTable(
                name: "Denuncia",
                schema: "dhr");

            migrationBuilder.DropTable(
                name: "Denunciante",
                schema: "dhr");

            migrationBuilder.DropTable(
                name: "PersonaAfectada",
                schema: "dhr");
        }
    }
}
