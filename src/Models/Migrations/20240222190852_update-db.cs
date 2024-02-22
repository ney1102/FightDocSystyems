using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Document_FlightId",
                table: "Document",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Flight_FlightId",
                table: "Document",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Flight_FlightId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_FlightId",
                table: "Document");
        }
    }
}
