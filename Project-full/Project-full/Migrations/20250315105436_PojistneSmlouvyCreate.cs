using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_full.Migrations
{
    /// <inheritdoc />
    public partial class PojistneSmlouvyCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DelkaPojisteni",
                table: "PojistneSmlouvy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PojistneSmlouvy_PojistenecId",
                table: "PojistneSmlouvy",
                column: "PojistenecId");

            migrationBuilder.CreateIndex(
                name: "IX_PojistneSmlouvy_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Osoby_PojistenecId",
                table: "PojistneSmlouvy",
                column: "PojistenecId",
                principalTable: "Osoby",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Osoby_PojistenecId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropIndex(
                name: "IX_PojistneSmlouvy_PojistenecId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropIndex(
                name: "IX_PojistneSmlouvy_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropColumn(
                name: "DelkaPojisteni",
                table: "PojistneSmlouvy");
        }
    }
}
