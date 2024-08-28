using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambiosWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_NewStateId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_PreviousStateId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserWfs_AspNetUsers_UserId",
                table: "GroupUserWfs");

            migrationBuilder.DropTable(
                name: "FlowGroupWfs");

            migrationBuilder.DropTable(
                name: "FlowStateWfs");

            migrationBuilder.DropTable(
                name: "StateActionWfs");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "StateWfs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlowId",
                table: "StateWfs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "GroupWfs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlowId",
                table: "GroupWfs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "FlowWfs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "FlowHistoryWfs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ActionWfs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "ActionWfs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ActionRuleWfs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateWfs_FlowId",
                table: "StateWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWfs_FlowId",
                table: "GroupWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionWfs_StateId",
                table: "ActionWfs",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionWfs_StateWfs_StateId",
                table: "ActionWfs",
                column: "StateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_NewStateId",
                table: "FlowHistoryWfs",
                column: "NewStateId",
                principalTable: "StateWfs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_PreviousStateId",
                table: "FlowHistoryWfs",
                column: "PreviousStateId",
                principalTable: "StateWfs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserWfs_AspNetUsers_UserId",
                table: "GroupUserWfs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWfs_FlowWfs_FlowId",
                table: "GroupWfs",
                column: "FlowId",
                principalTable: "FlowWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateWfs_FlowWfs_FlowId",
                table: "StateWfs",
                column: "FlowId",
                principalTable: "FlowWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionWfs_StateWfs_StateId",
                table: "ActionWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_NewStateId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_PreviousStateId",
                table: "FlowHistoryWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserWfs_AspNetUsers_UserId",
                table: "GroupUserWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupWfs_FlowWfs_FlowId",
                table: "GroupWfs");

            migrationBuilder.DropForeignKey(
                name: "FK_StateWfs_FlowWfs_FlowId",
                table: "StateWfs");

            migrationBuilder.DropIndex(
                name: "IX_StateWfs_FlowId",
                table: "StateWfs");

            migrationBuilder.DropIndex(
                name: "IX_GroupWfs_FlowId",
                table: "GroupWfs");

            migrationBuilder.DropIndex(
                name: "IX_ActionWfs_StateId",
                table: "ActionWfs");

            migrationBuilder.DropColumn(
                name: "FlowId",
                table: "StateWfs");

            migrationBuilder.DropColumn(
                name: "FlowId",
                table: "GroupWfs");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "ActionWfs");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "StateWfs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "GroupWfs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "FlowWfs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "FlowHistoryWfs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ActionWfs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ActionRuleWfs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "FlowGroupWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowGroupWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowGroupWfs_FlowWfs_FlowId",
                        column: x => x.FlowId,
                        principalTable: "FlowWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowGroupWfs_GroupWfs_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowStateWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStateWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowStateWfs_FlowWfs_FlowId",
                        column: x => x.FlowId,
                        principalTable: "FlowWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowStateWfs_StateWfs_StateId",
                        column: x => x.StateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateActionWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateActionWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateActionWfs_ActionWfs_StateId",
                        column: x => x.StateId,
                        principalTable: "ActionWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateActionWfs_StateWfs_StateId",
                        column: x => x.StateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroupWfs_FlowId",
                table: "FlowGroupWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroupWfs_GroupId",
                table: "FlowGroupWfs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStateWfs_FlowId",
                table: "FlowStateWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStateWfs_StateId",
                table: "FlowStateWfs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateActionWfs_StateId",
                table: "StateActionWfs",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_NewStateId",
                table: "FlowHistoryWfs",
                column: "NewStateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowHistoryWfs_StateWfs_PreviousStateId",
                table: "FlowHistoryWfs",
                column: "PreviousStateId",
                principalTable: "StateWfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserWfs_AspNetUsers_UserId",
                table: "GroupUserWfs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
