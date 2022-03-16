using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentGuess.Infrastructure.Migrations
{
    public partial class StartSecondsNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StartTimeSeconds",
                table: "ContentInfo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StartTimeSeconds",
                table: "ContentInfo",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
