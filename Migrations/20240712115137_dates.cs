using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicturePilot.Migrations
{
    /// <inheritdoc />
    public partial class dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImageHistory_AspNetUsers_UserId",
                table: "UserImageHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserImageHistory_Images_ImageId",
                table: "UserImageHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserImageHistory",
                table: "UserImageHistory");

            migrationBuilder.RenameTable(
                name: "UserImageHistory",
                newName: "History");

            migrationBuilder.RenameIndex(
                name: "IX_UserImageHistory_ImageId",
                table: "History",
                newName: "IX_History_ImageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Images",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                columns: new[] { "UserId", "ImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Images_ImageId",
                table: "History",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Images_ImageId",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "History",
                newName: "UserImageHistory");

            migrationBuilder.RenameIndex(
                name: "IX_History_ImageId",
                table: "UserImageHistory",
                newName: "IX_UserImageHistory_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserImageHistory",
                table: "UserImageHistory",
                columns: new[] { "UserId", "ImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserImageHistory_AspNetUsers_UserId",
                table: "UserImageHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImageHistory_Images_ImageId",
                table: "UserImageHistory",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
