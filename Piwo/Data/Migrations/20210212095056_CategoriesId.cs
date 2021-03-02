using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class CategoriesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_CategoriesId",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_CategoriesId",
                table: "Piwa");

            migrationBuilder.AddColumn<int>(
                name: "Categories",
                table: "Piwa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_Categories",
                table: "Piwa",
                column: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_Categories",
                table: "Piwa",
                column: "Categories",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_Categories",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_Categories",
                table: "Piwa");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Piwa");

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_CategoriesId",
                table: "Piwa",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_CategoriesId",
                table: "Piwa",
                column: "CategoriesId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
