using Microsoft.EntityFrameworkCore.Migrations;

namespace TelstarCES.Data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ParcelTypes_ParcelTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ParcelTypeId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_ParcelTypeId",
                table: "Orders",
                column: "ParcelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ParcelTypes_ParcelTypeId",
                table: "Orders",
                column: "ParcelTypeId",
                principalTable: "ParcelTypes",
                principalColumn: "ParcelTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
