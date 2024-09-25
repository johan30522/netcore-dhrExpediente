using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambiosTablasFlujo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_FlowWfs_FlowId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropTable(
                name: "RequestFlowHistoryWfs");

            migrationBuilder.DropIndex(
                name: "IX_FlowHistoryWfs_FlowId",
                table: "FlowHistoryWfs");

            migrationBuilder.RenameColumn(
                name: "FlowId",
                table: "FlowHistoryWfs",
                newName: "RequestFlowId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalState",
                table: "StateWfs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInitialState",
                table: "StateWfs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequestFlowHeaderId",
                table: "FlowHistoryWfs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FlowRequestHeaderWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    CurrentStateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowRequestHeaderWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowRequestHeaderWfs_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowRequestHeaderWfs_FlowWfs_FlowId",
                        column: x => x.FlowId,
                        principalTable: "FlowWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowRequestHeaderWfs_StateWfs_CurrentStateId",
                        column: x => x.CurrentStateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_RequestFlowHeaderId",
                table: "FlowHistoryWfs",
                column: "RequestFlowHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRequestHeaderWfs_CreatedByUserId",
                table: "FlowRequestHeaderWfs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRequestHeaderWfs_CurrentStateId",
                table: "FlowRequestHeaderWfs",
                column: "CurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRequestHeaderWfs_FlowId",
                table: "FlowRequestHeaderWfs",
                column: "FlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_FlowRequestHeaderWfs_RequestFlowHeaderId",
                table: "FlowHistoryWfs",
                column: "RequestFlowHeaderId",
                principalTable: "FlowRequestHeaderWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_FlowRequestHeaderWfs_RequestFlowHeaderId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropTable(
                name: "FlowRequestHeaderWfs");

            migrationBuilder.DropIndex(
                name: "IX_FlowHistoryWfs_RequestFlowHeaderId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropColumn(
                name: "IsFinalState",
                table: "StateWfs");

            migrationBuilder.DropColumn(
                name: "IsInitialState",
                table: "StateWfs");

            migrationBuilder.DropColumn(
                name: "RequestFlowHeaderId",
                table: "FlowHistoryWfs");

            migrationBuilder.RenameColumn(
                name: "RequestFlowId",
                table: "FlowHistoryWfs",
                newName: "FlowId");

            migrationBuilder.CreateTable(
                name: "RequestFlowHistoryWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowHistoryId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFlowHistoryWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestFlowHistoryWfs_FlowHistoryWfs_FlowHistoryId",
                        column: x => x.FlowHistoryId,
                        principalTable: "FlowHistoryWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_FlowId",
                table: "FlowHistoryWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFlowHistoryWfs_FlowHistoryId",
                table: "RequestFlowHistoryWfs",
                column: "FlowHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_FlowWfs_FlowId",
                table: "FlowHistoryWfs",
                column: "FlowId",
                principalTable: "FlowWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
