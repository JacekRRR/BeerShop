using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class LessReguired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Piwa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Piwa",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
