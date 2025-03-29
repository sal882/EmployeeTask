using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTask.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "AuditLogs",
                newName: "EntityAffected");

            migrationBuilder.RenameColumn(
                name: "OldValues",
                table: "AuditLogs",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "NewValues",
                table: "AuditLogs",
                newName: "Action");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityAffected",
                table: "AuditLogs",
                newName: "TableName");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "AuditLogs",
                newName: "OldValues");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "AuditLogs",
                newName: "NewValues");

            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
