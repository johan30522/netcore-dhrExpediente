using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "adm");

            migrationBuilder.RenameTable(
                name: "Eventos",
                schema: "admin",
                newName: "Eventos",
                newSchema: "adm");

            migrationBuilder.RenameTable(
                name: "Especifidades",
                schema: "admin",
                newName: "Especifidades",
                newSchema: "adm");

            migrationBuilder.RenameTable(
                name: "Descriptores",
                schema: "admin",
                newName: "Descriptores",
                newSchema: "adm");

            migrationBuilder.RenameTable(
                name: "Derechos",
                schema: "admin",
                newName: "Derechos",
                newSchema: "adm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");

            migrationBuilder.RenameTable(
                name: "Eventos",
                schema: "adm",
                newName: "Eventos",
                newSchema: "admin");

            migrationBuilder.RenameTable(
                name: "Especifidades",
                schema: "adm",
                newName: "Especifidades",
                newSchema: "admin");

            migrationBuilder.RenameTable(
                name: "Descriptores",
                schema: "adm",
                newName: "Descriptores",
                newSchema: "admin");

            migrationBuilder.RenameTable(
                name: "Derechos",
                schema: "adm",
                newName: "Derechos",
                newSchema: "admin");
        }
    }
}
