using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentGuess.Infrastructure.Migrations
{
    public partial class authorNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentInfo_Author_AuthorId",
                table: "ContentInfo");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "ContentInfo",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentInfo_Author_AuthorId",
                table: "ContentInfo",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentInfo_Author_AuthorId",
                table: "ContentInfo");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "ContentInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentInfo_Author_AuthorId",
                table: "ContentInfo",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
