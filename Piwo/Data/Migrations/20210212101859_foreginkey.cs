using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class foreginkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_CategoryId",
                table: "Piwa");

            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Tags_SpecialTagId",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_CategoryId",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_SpecialTagId",
                table: "Piwa");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Piwa");

            migrationBuilder.AddColumn<int>(
                name: "specialTagsId",
                table: "Piwa",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_CategoriesId",
                table: "Piwa",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_specialTagsId",
                table: "Piwa",
                column: "specialTagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_CategoriesId",
                table: "Piwa",
                column: "CategoriesId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Tags_specialTagsId",
                table: "Piwa",
                column: "specialTagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Kategorie_CategoriesId",
                table: "Piwa");

            migrationBuilder.DropForeignKey(
                name: "FK_Piwa_Tags_specialTagsId",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_CategoriesId",
                table: "Piwa");

            migrationBuilder.DropIndex(
                name: "IX_Piwa_specialTagsId",
                table: "Piwa");

            migrationBuilder.DropColumn(
                name: "specialTagsId",
                table: "Piwa");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Piwa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_CategoryId",
                table: "Piwa",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_SpecialTagId",
                table: "Piwa",
                column: "SpecialTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Kategorie_CategoryId",
                table: "Piwa",
                column: "CategoryId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Piwa_Tags_SpecialTagId",
                table: "Piwa",
                column: "SpecialTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
