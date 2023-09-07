using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailUsernameBlogComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Users_AuthorId",
                table: "BlogComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogComments_AuthorId",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "BlogComments");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BlogComments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BlogComments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "BlogComments");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "BlogComments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_AuthorId",
                table: "BlogComments",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Users_AuthorId",
                table: "BlogComments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
