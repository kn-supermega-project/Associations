using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Associations.DataAccess.Migrations
{
    public partial class MigrateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordRels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WordId = table.Column<int>(nullable: false),
                    WordRelId = table.Column<int>(nullable: false),
                    MalePoints = table.Column<int>(nullable: false),
                    FemalePoints = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<int>(nullable: false),
                    MalePercents = table.Column<float>(nullable: false),
                    FemalePercents = table.Column<float>(nullable: false),
                    TotalPercents = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordRels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordRels_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordRels_WordId",
                table: "WordRels",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordRels");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
