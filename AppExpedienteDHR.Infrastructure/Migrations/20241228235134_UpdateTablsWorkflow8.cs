using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablsWorkflow8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateWfs_StateNotificationsWfs_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropIndex(
                name: "IX_StateWfs_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropColumn(
                name: "StateNotificationId",
                table: "StateWfs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateNotificationId",
                table: "StateWfs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateWfs_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateWfs_StateNotificationsWfs_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId",
                principalTable: "StateNotificationsWfs",
                principalColumn: "Id");
        }
    }
}
