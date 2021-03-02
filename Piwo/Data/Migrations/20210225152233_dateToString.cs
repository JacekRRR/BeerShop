using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piwo.Data.Migrations
{
    public partial class dateToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderDate",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
