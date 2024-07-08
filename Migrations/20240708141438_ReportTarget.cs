using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicturePilot.Migrations
{
    /// <inheritdoc />
    public partial class ReportTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Reports");
        }
    }
}
