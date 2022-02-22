using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rating.Infrastructure.Migrations
{
    public partial class RatingDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "UserContentRatings",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Content",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "UserContentRatings",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<byte>(
                name: "AverageRating",
                table: "Content",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
