using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ContentGuess.Infrastructure.Migrations
{
    public partial class normalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "ContentInfoId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentTypeId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTimeSeconds = table.Column<double>(type: "double precision", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentInfo_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentInfoId",
                table: "Contents",
                column: "ContentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentInfo_AuthorId",
                table: "ContentInfo",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentInfo_ContentInfoId",
                table: "Contents",
                column: "ContentInfoId",
                principalTable: "ContentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentType_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId",
                principalTable: "ContentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentInfo_ContentInfoId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentType_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "ContentInfo");

            migrationBuilder.DropTable(
                name: "ContentType");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentInfoId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContentInfoId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Contents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Contents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
