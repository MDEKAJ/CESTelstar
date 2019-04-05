using Microsoft.EntityFrameworkCore.Migrations;

namespace TelstarCES.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Orders_OrderId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_Segments_OrderId",
                table: "Segments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Segments_OrderId",
                table: "Segments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_Orders_OrderId",
                table: "Segments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
