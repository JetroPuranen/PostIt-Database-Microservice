using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPostsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UsersId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UsersId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Posts",
                type: "uniqueidentifier",
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
    }
}
