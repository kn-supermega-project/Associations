using Microsoft.EntityFrameworkCore.Migrations;

namespace Associations.DataAccess.Migrations
{
    public partial class AddUniq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WordRels_WordId",
                table: "WordRels");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_MultipleColumns",
                table: "WordRels",
                columns: new[] { "WordId", "WordRelId" });

            migrationBuilder.CreateIndex(
                name: "IX_WordRels_WordId_WordRelId",
                table: "WordRels",
                columns: new[] { "WordId", "WordRelId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "IX_MultipleColumns",
                table: "WordRels");

            migrationBuilder.DropIndex(
                name: "IX_WordRels_WordId_WordRelId",
                table: "WordRels");

            migrationBuilder.CreateIndex(
                name: "IX_WordRels_WordId",
                table: "WordRels",
                column: "WordId");
        }
    }
}
