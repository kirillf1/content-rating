using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rating.Infrastructure.Migrations
{
    public partial class ContentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Content");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Content",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Content");

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Content",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
