using Microsoft.EntityFrameworkCore.Migrations;

namespace TelstarCES.Data.Migrations
{
    public partial class FixrelationbetweenCityandConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Cities_City1Id",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Cities_City2Id",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_City1Id",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_City2Id",
                table: "Connections");

            migrationBuilder.AlterColumn<int>(
                name: "City2Id",
                table: "Connections",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "City1Id",
                table: "Connections",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "City2Id",
                table: "Connections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "City1Id",
                table: "Connections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Connections_City1Id",
                table: "Connections",
                column: "City1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_City2Id",
                table: "Connections",
                column: "City2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Cities_City1Id",
                table: "Connections",
                column: "City1Id",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Cities_City2Id",
                table: "Connections",
                column: "City2Id",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
