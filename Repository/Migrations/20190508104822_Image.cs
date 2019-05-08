using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Notes");
        }
    }
}
