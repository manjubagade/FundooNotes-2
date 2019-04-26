using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetNotes",
                table: "GetNotes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "GetNotes",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "GetNotes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GetNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GetNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetNotes",
                table: "GetNotes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetNotes",
                table: "GetNotes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GetNotes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GetNotes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GetNotes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GetNotes",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetNotes",
                table: "GetNotes",
                column: "UserId");
        }
    }
}
