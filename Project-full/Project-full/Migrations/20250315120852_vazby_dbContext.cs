using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_full.Migrations
{
    /// <inheritdoc />
    public partial class vazby_dbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_AspNetUsers_PojistenecId1",
                table: "PojistneSmlouvy");

            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropIndex(
                name: "IX_PojistneSmlouvy_PojistenecId1",
                table: "PojistneSmlouvy");

            migrationBuilder.DropColumn(
                name: "PojistenecId1",
                table: "PojistneSmlouvy");

            migrationBuilder.AlterColumn<string>(
                name: "PojistenecId",
                table: "PojistneSmlouvy",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PojistneSmlouvy_PojistenecId",
                table: "PojistneSmlouvy",
                column: "PojistenecId");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_AspNetUsers_PojistenecId",
                table: "PojistneSmlouvy",
                column: "PojistenecId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_AspNetUsers_PojistenecId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy");

            migrationBuilder.DropIndex(
                name: "IX_PojistneSmlouvy_PojistenecId",
                table: "PojistneSmlouvy");

            migrationBuilder.AlterColumn<int>(
                name: "PojistenecId",
                table: "PojistneSmlouvy",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PojistenecId1",
                table: "PojistneSmlouvy",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PojistneSmlouvy_PojistenecId1",
                table: "PojistneSmlouvy",
                column: "PojistenecId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_AspNetUsers_PojistenecId1",
                table: "PojistneSmlouvy",
                column: "PojistenecId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PojistneSmlouvy_Pojisteni_PojisteniId",
                table: "PojistneSmlouvy",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "Id");
        }
    }
}
