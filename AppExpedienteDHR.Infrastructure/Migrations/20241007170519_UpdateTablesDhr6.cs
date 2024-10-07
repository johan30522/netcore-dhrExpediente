using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    public partial class UpdateTablesDhr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Eliminar el índice Full-Text fuera de la transacción
            migrationBuilder.Sql("DROP FULLTEXT INDEX ON dhr.Expedientes", suppressTransaction: true);

            // 2. Modificar la columna 'Detalle' (cambiando de 1000 a 2000 caracteres)
            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                schema: "dhr",
                table: "Expedientes",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            // 3. Agregar nuevas columnas o relaciones según tu cambio original
            migrationBuilder.AddColumn<int>(
                name: "PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Petitoria",
                schema: "dhr",
                table: "Expedientes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes",
                column: "PersonaAfectadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_PersonasAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes",
                column: "PersonaAfectadaId",
                principalSchema: "dhr",
                principalTable: "PersonasAfectada",
                principalColumn: "Id");

            // 4. Volver a crear el índice Full-Text fuera de la transacción
            migrationBuilder.Sql(@"
                CREATE FULLTEXT INDEX ON dhr.Expedientes(Detalle)
                KEY INDEX PK_Expedientes
                ON FullTextCatalog
                WITH STOPLIST = SYSTEM;
            ", suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Eliminar el índice Full-Text en el Down, también fuera de la transacción
            migrationBuilder.Sql("DROP FULLTEXT INDEX ON dhr.Expedientes", suppressTransaction: true);

            // 2. Revertir las columnas modificadas
            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_PersonasAfectada_PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "PersonaAfectadaId",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "Petitoria",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                schema: "dhr",
                table: "Expedientes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            // 3. Volver a crear el índice Full-Text en el Down, también fuera de la transacción
            migrationBuilder.Sql(@"
                CREATE FULLTEXT INDEX ON dhr.Expedientes(Detalle)
                KEY INDEX PK_Expedientes
                ON FullTextCatalog
                WITH STOPLIST = SYSTEM;
            ", suppressTransaction: true);
        }
    }
}