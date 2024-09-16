using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablesDenuncia01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncia_Denunciante_DenuncianteId",
                schema: "dhr",
                table: "Denuncia");

            migrationBuilder.DropForeignKey(
                name: "FK_Denuncia_PersonaAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncia");

            migrationBuilder.DropForeignKey(
                name: "FK_DenunciaAdjuntos_Denuncia_DenunciaId",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Cantones_CantonCodigo",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Distritos_DistritoCodigo",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Escolaridad_EscolaridadId",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_EstadoCivil_EstadoCivilId",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Paises_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Provincias_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_Sexo_SexoId",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciante_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Denuncia_DenunciaId",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Denunciante_DenuncianteId",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaAfectada_Sexo_SexoId",
                schema: "dhr",
                table: "PersonaAfectada");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaAfectada_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonaAfectada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonaAfectada",
                schema: "dhr",
                table: "PersonaAfectada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denunciante",
                schema: "dhr",
                table: "Denunciante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denuncia",
                schema: "dhr",
                table: "Denuncia");

            migrationBuilder.RenameTable(
                name: "Expedientes",
                newName: "Expedientes",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "DenunciaAdjuntos",
                newName: "DenunciaAdjuntos",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "PersonaAfectada",
                schema: "dhr",
                newName: "PersonasAfectada",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "Denunciante",
                schema: "dhr",
                newName: "Denunciantes",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "Denuncia",
                schema: "dhr",
                newName: "Denuncias",
                newSchema: "dhr");

            migrationBuilder.RenameIndex(
                name: "IX_PersonaAfectada_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonasAfectada",
                newName: "IX_PersonasAfectada_TipoIdentificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonaAfectada_SexoId",
                schema: "dhr",
                table: "PersonasAfectada",
                newName: "IX_PersonasAfectada_SexoId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_TipoIdentificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_SexoId",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_SexoId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_ProvinciaCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_PaisOrigenCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_EstadoCivilId",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_EstadoCivilId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_EscolaridadId",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_EscolaridadId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_DistritoCodigo",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_DistritoCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciante_CantonCodigo",
                schema: "dhr",
                table: "Denunciantes",
                newName: "IX_Denunciantes_CantonCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncia_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncias",
                newName: "IX_Denuncias_PersonaAfectadaId");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncia_DenuncianteId",
                schema: "dhr",
                table: "Denuncias",
                newName: "IX_Denuncias_DenuncianteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonasAfectada",
                schema: "dhr",
                table: "PersonasAfectada",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denunciantes",
                schema: "dhr",
                table: "Denunciantes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denuncias",
                schema: "dhr",
                table: "Denuncias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DenunciaAdjuntos_Denuncias_DenunciaId",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                column: "DenunciaId",
                principalSchema: "dhr",
                principalTable: "Denuncias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Cantones_CantonCodigo",
                schema: "dhr",
                table: "Denunciantes",
                column: "CantonCodigo",
                principalSchema: "gen",
                principalTable: "Cantones",
                principalColumn: "CodigoCanton",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Distritos_DistritoCodigo",
                schema: "dhr",
                table: "Denunciantes",
                column: "DistritoCodigo",
                principalSchema: "gen",
                principalTable: "Distritos",
                principalColumn: "CodigoDistrito",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Escolaridad_EscolaridadId",
                schema: "dhr",
                table: "Denunciantes",
                column: "EscolaridadId",
                principalSchema: "gen",
                principalTable: "Escolaridad",
                principalColumn: "EscolaridadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_EstadoCivil_EstadoCivilId",
                schema: "dhr",
                table: "Denunciantes",
                column: "EstadoCivilId",
                principalSchema: "gen",
                principalTable: "EstadoCivil",
                principalColumn: "EstadoCivilId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Paises_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciantes",
                column: "PaisOrigenCodigo",
                principalSchema: "gen",
                principalTable: "Paises",
                principalColumn: "CodigoNumerico",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Provincias_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciantes",
                column: "ProvinciaCodigo",
                principalSchema: "gen",
                principalTable: "Provincias",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_Sexo_SexoId",
                schema: "dhr",
                table: "Denunciantes",
                column: "SexoId",
                principalSchema: "gen",
                principalTable: "Sexo",
                principalColumn: "SexoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciantes_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciantes",
                column: "TipoIdentificacionId",
                principalSchema: "gen",
                principalTable: "TipoIdentificacion",
                principalColumn: "TipoIdentificacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Denunciantes_DenuncianteId",
                schema: "dhr",
                table: "Denuncias",
                column: "DenuncianteId",
                principalSchema: "dhr",
                principalTable: "Denunciantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_PersonasAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncias",
                column: "PersonaAfectadaId",
                principalSchema: "dhr",
                principalTable: "PersonasAfectada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Denunciantes_DenuncianteId",
                schema: "dhr",
                table: "Expedientes",
                column: "DenuncianteId",
                principalSchema: "dhr",
                principalTable: "Denunciantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Denuncias_DenunciaId",
                schema: "dhr",
                table: "Expedientes",
                column: "DenunciaId",
                principalSchema: "dhr",
                principalTable: "Denuncias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasAfectada_Sexo_SexoId",
                schema: "dhr",
                table: "PersonasAfectada",
                column: "SexoId",
                principalSchema: "gen",
                principalTable: "Sexo",
                principalColumn: "SexoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasAfectada_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonasAfectada",
                column: "TipoIdentificacionId",
                principalSchema: "gen",
                principalTable: "TipoIdentificacion",
                principalColumn: "TipoIdentificacionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DenunciaAdjuntos_Denuncias_DenunciaId",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Cantones_CantonCodigo",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Distritos_DistritoCodigo",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Escolaridad_EscolaridadId",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_EstadoCivil_EstadoCivilId",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Paises_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Provincias_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_Sexo_SexoId",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denunciantes_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Denunciantes_DenuncianteId",
                schema: "dhr",
                table: "Denuncias");

            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_PersonasAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncias");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Denunciantes_DenuncianteId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Denuncias_DenunciaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonasAfectada_Sexo_SexoId",
                schema: "dhr",
                table: "PersonasAfectada");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonasAfectada_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonasAfectada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonasAfectada",
                schema: "dhr",
                table: "PersonasAfectada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denuncias",
                schema: "dhr",
                table: "Denuncias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denunciantes",
                schema: "dhr",
                table: "Denunciantes");

            migrationBuilder.RenameTable(
                name: "Expedientes",
                schema: "dhr",
                newName: "Expedientes");

            migrationBuilder.RenameTable(
                name: "DenunciaAdjuntos",
                schema: "dhr",
                newName: "DenunciaAdjuntos");

            migrationBuilder.RenameTable(
                name: "PersonasAfectada",
                schema: "dhr",
                newName: "PersonaAfectada",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "Denuncias",
                schema: "dhr",
                newName: "Denuncia",
                newSchema: "dhr");

            migrationBuilder.RenameTable(
                name: "Denunciantes",
                schema: "dhr",
                newName: "Denunciante",
                newSchema: "dhr");

            migrationBuilder.RenameIndex(
                name: "IX_PersonasAfectada_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonaAfectada",
                newName: "IX_PersonaAfectada_TipoIdentificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonasAfectada_SexoId",
                schema: "dhr",
                table: "PersonaAfectada",
                newName: "IX_PersonaAfectada_SexoId");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncias_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncia",
                newName: "IX_Denuncia_PersonaAfectadaId");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncias_DenuncianteId",
                schema: "dhr",
                table: "Denuncia",
                newName: "IX_Denuncia_DenuncianteId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_TipoIdentificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_SexoId",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_SexoId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_ProvinciaCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_PaisOrigenCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_EstadoCivilId",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_EstadoCivilId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_EscolaridadId",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_EscolaridadId");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_DistritoCodigo",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_DistritoCodigo");

            migrationBuilder.RenameIndex(
                name: "IX_Denunciantes_CantonCodigo",
                schema: "dhr",
                table: "Denunciante",
                newName: "IX_Denunciante_CantonCodigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonaAfectada",
                schema: "dhr",
                table: "PersonaAfectada",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denuncia",
                schema: "dhr",
                table: "Denuncia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denunciante",
                schema: "dhr",
                table: "Denunciante",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncia_Denunciante_DenuncianteId",
                schema: "dhr",
                table: "Denuncia",
                column: "DenuncianteId",
                principalSchema: "dhr",
                principalTable: "Denunciante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncia_PersonaAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Denuncia",
                column: "PersonaAfectadaId",
                principalSchema: "dhr",
                principalTable: "PersonaAfectada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DenunciaAdjuntos_Denuncia_DenunciaId",
                table: "DenunciaAdjuntos",
                column: "DenunciaId",
                principalSchema: "dhr",
                principalTable: "Denuncia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Cantones_CantonCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "CantonCodigo",
                principalSchema: "gen",
                principalTable: "Cantones",
                principalColumn: "CodigoCanton",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Distritos_DistritoCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "DistritoCodigo",
                principalSchema: "gen",
                principalTable: "Distritos",
                principalColumn: "CodigoDistrito",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Escolaridad_EscolaridadId",
                schema: "dhr",
                table: "Denunciante",
                column: "EscolaridadId",
                principalSchema: "gen",
                principalTable: "Escolaridad",
                principalColumn: "EscolaridadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_EstadoCivil_EstadoCivilId",
                schema: "dhr",
                table: "Denunciante",
                column: "EstadoCivilId",
                principalSchema: "gen",
                principalTable: "EstadoCivil",
                principalColumn: "EstadoCivilId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Paises_PaisOrigenCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "PaisOrigenCodigo",
                principalSchema: "gen",
                principalTable: "Paises",
                principalColumn: "CodigoNumerico",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Provincias_ProvinciaCodigo",
                schema: "dhr",
                table: "Denunciante",
                column: "ProvinciaCodigo",
                principalSchema: "gen",
                principalTable: "Provincias",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_Sexo_SexoId",
                schema: "dhr",
                table: "Denunciante",
                column: "SexoId",
                principalSchema: "gen",
                principalTable: "Sexo",
                principalColumn: "SexoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denunciante_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "Denunciante",
                column: "TipoIdentificacionId",
                principalSchema: "gen",
                principalTable: "TipoIdentificacion",
                principalColumn: "TipoIdentificacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Denuncia_DenunciaId",
                table: "Expedientes",
                column: "DenunciaId",
                principalSchema: "dhr",
                principalTable: "Denuncia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Denunciante_DenuncianteId",
                table: "Expedientes",
                column: "DenuncianteId",
                principalSchema: "dhr",
                principalTable: "Denunciante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaAfectada_Sexo_SexoId",
                schema: "dhr",
                table: "PersonaAfectada",
                column: "SexoId",
                principalSchema: "gen",
                principalTable: "Sexo",
                principalColumn: "SexoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaAfectada_TipoIdentificacion_TipoIdentificacionId",
                schema: "dhr",
                table: "PersonaAfectada",
                column: "TipoIdentificacionId",
                principalSchema: "gen",
                principalTable: "TipoIdentificacion",
                principalColumn: "TipoIdentificacionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
