using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablsWorkflow10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs");

            migrationBuilder.DropIndex(
                name: "IX_StateNotificationsWfs_EmailTemplateId",
                table: "StateNotificationsWfs");

            migrationBuilder.CreateIndex(
                name: "IX_StateNotificationsWfs_EmailTemplateId",
                table: "StateNotificationsWfs",
                column: "EmailTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs",
                column: "EmailTemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs");

            migrationBuilder.DropIndex(
                name: "IX_StateNotificationsWfs_EmailTemplateId",
                table: "StateNotificationsWfs");

            migrationBuilder.CreateIndex(
                name: "IX_StateNotificationsWfs_EmailTemplateId",
                table: "StateNotificationsWfs",
                column: "EmailTemplateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs",
                column: "EmailTemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
