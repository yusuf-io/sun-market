using Microsoft.EntityFrameworkCore.Migrations;

namespace SunMarket.Data.Migrations
{
    public partial class CorrectingIsTaxable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTaxabel",
                table: "Products",
                newName: "IsTaxable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTaxable",
                table: "Products",
                newName: "IsTaxabel");
        }
    }
}
