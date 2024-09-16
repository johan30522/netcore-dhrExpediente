using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdjuntos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSubida",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropColumn(
                name: "NombreArchivo",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.AddColumn<int>(
                name: "AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Adjuntos",
                schema: "dhr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreOriginal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjuntos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DenunciaAdjuntos_AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                column: "AdjuntoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DenunciaAdjuntos_Adjuntos_AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                column: "AdjuntoId",
                principalSchema: "dhr",
                principalTable: "Adjuntos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DenunciaAdjuntos_Adjuntos_AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropTable(
                name: "Adjuntos",
                schema: "dhr");

            migrationBuilder.DropIndex(
                name: "IX_DenunciaAdjuntos_AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.DropColumn(
                name: "AdjuntoId",
                schema: "dhr",
                table: "DenunciaAdjuntos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSubida",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombreArchivo",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                schema: "dhr",
                table: "DenunciaAdjuntos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
