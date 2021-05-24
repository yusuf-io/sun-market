using Microsoft.EntityFrameworkCore.Migrations;

namespace SunMarket.Data.Migrations
{
    public partial class FixQuantityOnHand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityOnHund",
                table: "ProductInventories",
                newName: "QuantityOnHand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityOnHand",
                table: "ProductInventories",
                newName: "QuantityOnHund");
        }
    }
}
