using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinorRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhoHasLiked",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UsersId",
                table: "Posts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UsersId",
                table: "Posts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UsersId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UsersId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "WhoHasLiked",
                table: "Posts");
        }
    }
}
