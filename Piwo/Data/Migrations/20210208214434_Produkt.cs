using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class Produkt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piwa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Brewery = table.Column<string>(nullable: false),
                    alcoholContent = table.Column<float>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    IsAvalible = table.Column<bool>(nullable: false),
                    NumberOfItems = table.Column<int>(nullable: true),
                    CategoriesId = table.Column<int>(nullable: false),
                    SpecialTagId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piwa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piwa_Kategorie_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piwa_Tags_SpecialTagId",
                        column: x => x.SpecialTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_CategoriesId",
                table: "Piwa",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Piwa_SpecialTagId",
                table: "Piwa",
                column: "SpecialTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piwa");
        }
    }
}
