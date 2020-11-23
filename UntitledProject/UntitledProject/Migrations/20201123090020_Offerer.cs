using Microsoft.EntityFrameworkCore.Migrations;

namespace UntitledProject.Migrations
{
    public partial class Offerer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offerer",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffererId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_OffererId",
                table: "Product",
                column: "OffererId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_OffererId",
                table: "Product",
                column: "OffererId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_OffererId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OffererId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OffererId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Offerer",
                table: "Product",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}
