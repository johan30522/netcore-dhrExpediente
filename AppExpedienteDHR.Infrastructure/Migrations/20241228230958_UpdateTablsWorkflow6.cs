using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablsWorkflow6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications",
                column: "StateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications",
                column: "StateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
