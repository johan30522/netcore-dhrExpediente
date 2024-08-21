using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkflowTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowWfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupWfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateWfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowGroupWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "GroupUserWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUserWfs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserWfs_GroupWfs_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    NextStateId = table.Column<int>(type: "int", nullable: true),
                    EvaluationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionWfs_StateWfs_NextStateId",
                        column: x => x.NextStateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlowStateWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "ActionGroupWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionGroupWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionGroupWfs_ActionWfs_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ActionWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionGroupWfs_GroupWfs_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionRuleWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    FieldEvaluated = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ComparisonValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ResultStateId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRuleWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionRuleWfs_ActionWfs_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ActionWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionRuleWfs_StateWfs_ResultStateId",
                        column: x => x.ResultStateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowHistoryWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    PreviousStateId = table.Column<int>(type: "int", nullable: false),
                    NewStateId = table.Column<int>(type: "int", nullable: false),
                    ActionPerformedId = table.Column<int>(type: "int", nullable: false),
                    PerformedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowHistoryWfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowHistoryWfs_ActionWfs_ActionPerformedId",
                        column: x => x.ActionPerformedId,
                        principalTable: "ActionWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowHistoryWfs_AspNetUsers_PerformedByUserId",
                        column: x => x.PerformedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowHistoryWfs_FlowWfs_FlowId",
                        column: x => x.FlowId,
                        principalTable: "FlowWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowHistoryWfs_StateWfs_NewStateId",
                        column: x => x.NewStateId,
                        principalTable: "StateWfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowHistoryWfs_StateWfs_PreviousStateId",
                        column: x => x.PreviousStateId,
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "RequestFlowHistoryWfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    FlowHistoryId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ActionGroupWfs_ActionId",
                table: "ActionGroupWfs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionGroupWfs_GroupId",
                table: "ActionGroupWfs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRuleWfs_ActionId",
                table: "ActionRuleWfs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionRuleWfs_ResultStateId",
                table: "ActionRuleWfs",
                column: "ResultStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionWfs_NextStateId",
                table: "ActionWfs",
                column: "NextStateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroupWfs_FlowId",
                table: "FlowGroupWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowGroupWfs_GroupId",
                table: "FlowGroupWfs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_ActionPerformedId",
                table: "FlowHistoryWfs",
                column: "ActionPerformedId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_FlowId",
                table: "FlowHistoryWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_NewStateId",
                table: "FlowHistoryWfs",
                column: "NewStateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_PerformedByUserId",
                table: "FlowHistoryWfs",
                column: "PerformedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowHistoryWfs_PreviousStateId",
                table: "FlowHistoryWfs",
                column: "PreviousStateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStateWfs_FlowId",
                table: "FlowStateWfs",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStateWfs_StateId",
                table: "FlowStateWfs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserWfs_GroupId",
                table: "GroupUserWfs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserWfs_UserId",
                table: "GroupUserWfs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFlowHistoryWfs_FlowHistoryId",
                table: "RequestFlowHistoryWfs",
                column: "FlowHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StateActionWfs_StateId",
                table: "StateActionWfs",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionGroupWfs");

            migrationBuilder.DropTable(
                name: "ActionRuleWfs");

            migrationBuilder.DropTable(
                name: "FlowGroupWfs");

            migrationBuilder.DropTable(
                name: "FlowStateWfs");

            migrationBuilder.DropTable(
                name: "GroupUserWfs");

            migrationBuilder.DropTable(
                name: "RequestFlowHistoryWfs");

            migrationBuilder.DropTable(
                name: "StateActionWfs");

            migrationBuilder.DropTable(
                name: "GroupWfs");

            migrationBuilder.DropTable(
                name: "FlowHistoryWfs");

            migrationBuilder.DropTable(
                name: "ActionWfs");

            migrationBuilder.DropTable(
                name: "FlowWfs");

            migrationBuilder.DropTable(
                name: "StateWfs");
        }
    }
}
