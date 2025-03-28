using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_full.Migrations
{
    /// <inheritdoc />
    public partial class updateDbonDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
