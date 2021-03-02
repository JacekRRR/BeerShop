using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class specjalTagg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeOfProduct",
                table: "Tags",
                newName: "TagName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Tags",
                newName: "TypeOfProduct");
        }
    }
}
