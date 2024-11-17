using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableDhr9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DerechoId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EspecificidadId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventoId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DerechoId",
                schema: "dhr",
                table: "Expedientes",
                column: "DerechoId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_EspecificidadId",
                schema: "dhr",
                table: "Expedientes",
                column: "EspecificidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_EventoId",
                schema: "dhr",
                table: "Expedientes",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Derechos_DerechoId",
                schema: "dhr",
                table: "Expedientes",
                column: "DerechoId",
                principalSchema: "adm",
                principalTable: "Derechos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Especifidades_EspecificidadId",
                schema: "dhr",
                table: "Expedientes",
                column: "EspecificidadId",
                principalSchema: "adm",
                principalTable: "Especifidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Eventos_EventoId",
                schema: "dhr",
                table: "Expedientes",
                column: "EventoId",
                principalSchema: "adm",
                principalTable: "Eventos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Derechos_DerechoId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Especifidades_EspecificidadId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Eventos_EventoId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_DerechoId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_EspecificidadId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_EventoId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "DerechoId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "EspecificidadId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "EventoId",
                schema: "dhr",
                table: "Expedientes");
        }
    }
}
