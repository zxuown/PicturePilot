using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicturePilot.Migrations
{
    /// <inheritdoc />
    public partial class IsBlockedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBLocked",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBLocked",
                table: "AspNetUsers");
        }
    }
}
