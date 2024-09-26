using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesDhr4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expedientes_DenunciaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.AlterColumn<int>(
                name: "DenunciaId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DenunciaId",
                schema: "dhr",
                table: "Expedientes",
                column: "DenunciaId",
                unique: true,
                filter: "[DenunciaId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expedientes_DenunciaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.AlterColumn<int>(
                name: "DenunciaId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DenunciaId",
                schema: "dhr",
                table: "Expedientes",
                column: "DenunciaId",
                unique: true);
        }
    }
}
