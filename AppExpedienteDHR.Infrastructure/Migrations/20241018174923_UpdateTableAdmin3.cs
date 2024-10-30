using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAdmin3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "adm",
                table: "Eventos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "adm",
                table: "Eventos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "adm",
                table: "Especifidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "adm",
                table: "Especifidades",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "adm",
                table: "Descriptores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "adm",
                table: "Descriptores",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "adm",
                table: "Derechos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "adm",
                table: "Derechos",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "adm",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "adm",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "adm",
                table: "Especifidades");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "adm",
                table: "Especifidades");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "adm",
                table: "Descriptores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "adm",
                table: "Descriptores");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "adm",
                table: "Derechos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "adm",
                table: "Derechos");
        }
    }
}
