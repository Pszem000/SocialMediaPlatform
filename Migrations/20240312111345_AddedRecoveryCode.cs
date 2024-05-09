using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Migrations
{
    public partial class AddedRecoveryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "ImageList",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "RecoveryCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryCode",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ImageList",
                newName: "id");
        }
    }
}
