using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablsWorkflow7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroups_GroupWfs_GroupId",
                table: "NotificationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StateNotifications_EmailTemplates_EmailTemplateId",
                table: "StateNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_StateWfs_StateNotifications_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateNotifications",
                table: "StateNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationGroups",
                table: "NotificationGroups");

            migrationBuilder.RenameTable(
                name: "StateNotifications",
                newName: "StateNotificationsWfs");

            migrationBuilder.RenameTable(
                name: "NotificationGroups",
                newName: "NotificationGroupsWfs");

            migrationBuilder.RenameIndex(
                name: "IX_StateNotifications_StateId",
                table: "StateNotificationsWfs",
                newName: "IX_StateNotificationsWfs_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_StateNotifications_EmailTemplateId",
                table: "StateNotificationsWfs",
                newName: "IX_StateNotificationsWfs_EmailTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationGroups_NotificationId",
                table: "NotificationGroupsWfs",
                newName: "IX_NotificationGroupsWfs_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationGroups_GroupId",
                table: "NotificationGroupsWfs",
                newName: "IX_NotificationGroupsWfs_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateNotificationsWfs",
                table: "StateNotificationsWfs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationGroupsWfs",
                table: "NotificationGroupsWfs",
                columns: new[] { "GroupId", "NotificationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroupsWfs_GroupWfs_GroupId",
                table: "NotificationGroupsWfs",
                column: "GroupId",
                principalTable: "GroupWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroupsWfs_StateNotificationsWfs_NotificationId",
                table: "NotificationGroupsWfs",
                column: "NotificationId",
                principalTable: "StateNotificationsWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs",
                column: "EmailTemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotificationsWfs_StateWfs_StateId",
                table: "StateNotificationsWfs",
                column: "StateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateWfs_StateNotificationsWfs_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId",
                principalTable: "StateNotificationsWfs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroupsWfs_GroupWfs_GroupId",
                table: "NotificationGroupsWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationGroupsWfs_StateNotificationsWfs_NotificationId",
                table: "NotificationGroupsWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_StateNotificationsWfs_EmailTemplates_EmailTemplateId",
                table: "StateNotificationsWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_StateNotificationsWfs_StateWfs_StateId",
                table: "StateNotificationsWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_StateWfs_StateNotificationsWfs_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateNotificationsWfs",
                table: "StateNotificationsWfs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationGroupsWfs",
                table: "NotificationGroupsWfs");

            migrationBuilder.RenameTable(
                name: "StateNotificationsWfs",
                newName: "StateNotifications");

            migrationBuilder.RenameTable(
                name: "NotificationGroupsWfs",
                newName: "NotificationGroups");

            migrationBuilder.RenameIndex(
                name: "IX_StateNotificationsWfs_StateId",
                table: "StateNotifications",
                newName: "IX_StateNotifications_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_StateNotificationsWfs_EmailTemplateId",
                table: "StateNotifications",
                newName: "IX_StateNotifications_EmailTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationGroupsWfs_NotificationId",
                table: "NotificationGroups",
                newName: "IX_NotificationGroups_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationGroupsWfs_GroupId",
                table: "NotificationGroups",
                newName: "IX_NotificationGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateNotifications",
                table: "StateNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationGroups",
                table: "NotificationGroups",
                columns: new[] { "GroupId", "NotificationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroups_GroupWfs_GroupId",
                table: "NotificationGroups",
                column: "GroupId",
                principalTable: "GroupWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationGroups_StateNotifications_NotificationId",
                table: "NotificationGroups",
                column: "NotificationId",
                principalTable: "StateNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotifications_EmailTemplates_EmailTemplateId",
                table: "StateNotifications",
                column: "EmailTemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateNotifications_StateWfs_StateId",
                table: "StateNotifications",
                column: "StateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateWfs_StateNotifications_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId",
                principalTable: "StateNotifications",
                principalColumn: "Id");
        }
    }
}
