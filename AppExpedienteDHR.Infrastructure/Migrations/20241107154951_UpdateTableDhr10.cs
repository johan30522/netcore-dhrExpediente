using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableDhr10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptorId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_DescriptorId",
                schema: "dhr",
                table: "Expedientes",
                column: "DescriptorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Descriptores_DescriptorId",
                schema: "dhr",
                table: "Expedientes",
                column: "DescriptorId",
                principalSchema: "adm",
                principalTable: "Descriptores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Descriptores_DescriptorId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_DescriptorId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "DescriptorId",
                schema: "dhr",
                table: "Expedientes");
        }
    }
}
