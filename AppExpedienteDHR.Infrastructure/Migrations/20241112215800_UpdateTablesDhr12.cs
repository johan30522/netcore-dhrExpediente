using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesDhr12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpedienteAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpedienteId = table.Column<int>(type: "int", nullable: false),
                    AdjuntoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpedienteAdjuntos_Adjuntos_AdjuntoId",
                        column: x => x.AdjuntoId,
                        principalSchema: "dhr",
                        principalTable: "Adjuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpedienteAdjuntos_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalSchema: "dhr",
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAdjuntos_AdjuntoId",
                table: "ExpedienteAdjuntos",
                column: "AdjuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAdjuntos_ExpedienteId",
                table: "ExpedienteAdjuntos",
                column: "ExpedienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpedienteAdjuntos");
        }
    }
}
