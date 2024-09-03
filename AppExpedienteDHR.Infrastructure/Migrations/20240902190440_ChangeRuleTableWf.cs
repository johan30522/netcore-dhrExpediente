using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRuleTableWf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparisonValue",
                table: "ActionRuleWfs");

            migrationBuilder.DropColumn(
                name: "FieldEvaluated",
                table: "ActionRuleWfs");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "ActionRuleWfs");

            migrationBuilder.AddColumn<string>(
                name: "RuleJson",
                table: "ActionRuleWfs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleJson",
                table: "ActionRuleWfs");

            migrationBuilder.AddColumn<string>(
                name: "ComparisonValue",
                table: "ActionRuleWfs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FieldEvaluated",
                table: "ActionRuleWfs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "ActionRuleWfs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
