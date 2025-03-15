using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_full.Migrations
{
    /// <inheritdoc />
    public partial class PojistneSmlouvy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PojistenecId",
                table: "PojistneSmlouvy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PojistenecId",
                table: "PojistneSmlouvy");
        }
    }
}
