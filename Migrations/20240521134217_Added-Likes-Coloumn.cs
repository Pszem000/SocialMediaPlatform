using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaPlatform.Migrations
{
    public partial class AddedLikesColoumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostModelId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostModelId",
                table: "Users",
                column: "PostModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Posts_PostModelId",
                table: "Users",
                column: "PostModelId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Posts_PostModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PostModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "Users");
        }
    }
}
