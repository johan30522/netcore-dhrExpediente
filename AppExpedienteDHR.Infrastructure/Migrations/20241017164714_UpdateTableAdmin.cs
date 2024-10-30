using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");

            migrationBuilder.CreateTable(
                name: "Derechos",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derechos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Normativa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ODS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DerechoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Derechos_DerechoId",
                        column: x => x.DerechoId,
                        principalSchema: "admin",
                        principalTable: "Derechos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descriptores",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptores_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalSchema: "admin",
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especifidades",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Normativa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especifidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especifidades_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalSchema: "admin",
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descriptores_EventoId",
                schema: "admin",
                table: "Descriptores",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Especifidades_EventoId",
                schema: "admin",
                table: "Especifidades",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_DerechoId",
                schema: "admin",
                table: "Eventos",
                column: "DerechoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Descriptores",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Especifidades",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Derechos",
                schema: "admin");
        }
    }
}
