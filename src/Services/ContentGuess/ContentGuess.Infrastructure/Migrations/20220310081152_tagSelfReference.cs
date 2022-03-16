using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentGuess.Infrastructure.Migrations
{
    public partial class tagSelfReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentTagId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tags_ParentTagId",
                table: "Tags",
                column: "ParentTagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tags_ParentTagId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ParentTagId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ParentTagId",
                table: "Tags");
        }
    }
}
