using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserIdToAuthorIdOnBlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BlogPosts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_UserId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BlogPosts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
