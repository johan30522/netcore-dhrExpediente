using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableWorkflow2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateNotificationId",
                table: "StateWfs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StateNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    EmailTemplateId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateNotifications_EmailTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateNotifications_GroupWfs_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupWfs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StateNotifications_StateWfs_StateId",
                        column: x => x.StateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateWfs_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_StateNotifications_EmailTemplateId",
                table: "StateNotifications",
                column: "EmailTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateNotifications_GroupId",
                table: "StateNotifications",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StateNotifications_StateId",
                table: "StateNotifications",
                column: "StateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StateWfs_StateNotifications_StateNotificationId",
                table: "StateWfs",
                column: "StateNotificationId",
                principalTable: "StateNotifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateWfs_StateNotifications_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropTable(
                name: "StateNotifications");

            migrationBuilder.DropIndex(
                name: "IX_StateWfs_StateNotificationId",
                table: "StateWfs");

            migrationBuilder.DropColumn(
                name: "StateNotificationId",
                table: "StateWfs");
        }
    }
}
