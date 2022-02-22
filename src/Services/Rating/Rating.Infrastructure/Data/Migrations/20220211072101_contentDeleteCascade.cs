using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rating.Infrastructure.Migrations
{
    public partial class contentDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Rooms_RoomId",
                table: "Content");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Rooms_RoomId",
                table: "Content",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Rooms_RoomId",
                table: "Content");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Rooms_RoomId",
                table: "Content",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
