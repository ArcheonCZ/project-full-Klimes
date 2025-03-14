using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_full.Migrations
{
    /// <inheritdoc />
    public partial class FormRegistrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vek",
                table: "Osoby");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumNarozeni",
                table: "Osoby",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Osoby",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumNarozeni",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Osoby");

            migrationBuilder.AddColumn<int>(
                name: "Vek",
                table: "Osoby",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
