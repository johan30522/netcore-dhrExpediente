using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableWorkflow5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups");

            migrationBuilder.DropIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroups");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId",
                principalTable: "StateNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups");

            migrationBuilder.DropIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroups");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId",
                principalTable: "StateNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
