using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class Categoriess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_Categories",
                table: "Piwa");

            migrationBuilder.RenameColumn(
                name: "Categories",
                table: "Piwa",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Piwa_Categories",
                table: "Piwa",
                newName: "IX_Piwa_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_CategoryId",
                table: "Piwa",
                column: "CategoryId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_CategoryId",
                table: "Piwa");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Piwa",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Piwa_CategoryId",
                table: "Piwa",
                newName: "IX_Piwa_Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_Categories",
                table: "Piwa",
                column: "Categories",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
