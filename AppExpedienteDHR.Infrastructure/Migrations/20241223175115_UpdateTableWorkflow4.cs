using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableWorkflow4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateNotifications_GroupWfs_GroupId",
                table: "StateNotifications");

            migrationBuilder.DropIndex(
                name: "IX_StateNotifications_GroupId",
                table: "StateNotifications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "StateNotifications");

            migrationBuilder.CreateTable(
                name: "NotificationGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationGroups", x => new { x.GroupId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_NotificationGroups_GroupWfs_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationGroups_StateNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "StateNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroups_GroupId",
                table: "NotificationGroups",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationGroups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "StateNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateNotifications_GroupId",
                table: "StateNotifications",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotifications_GroupWfs_GroupId",
                table: "StateNotifications",
                column: "GroupId",
                principalTable: "GroupWfs",
                principalColumn: "Id");
        }
    }
}
