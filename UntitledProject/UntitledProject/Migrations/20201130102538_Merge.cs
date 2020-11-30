using Microsoft.EntityFrameworkCore.Migrations;

namespace UntitledProject.Migrations
{
    public partial class Merge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "AspNetUsers");
        }
    }
}
