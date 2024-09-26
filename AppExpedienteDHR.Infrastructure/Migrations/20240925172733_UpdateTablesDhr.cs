using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesDhr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "dhr",
                table: "Expedientes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dhr",
                table: "Expedientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "dhr",
                table: "Denuncias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dhr",
                table: "Denuncias",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dhr",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "dhr",
                table: "Denuncias");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dhr",
                table: "Denuncias");
        }
    }
}
