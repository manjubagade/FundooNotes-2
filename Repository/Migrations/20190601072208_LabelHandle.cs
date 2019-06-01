using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Labels_LabelId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_LabelId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Notes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LabelId",
                table: "Notes",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Labels_LabelId",
                table: "Notes",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
