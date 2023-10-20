using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    public partial class btman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityForCartPro",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "totalQuantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalQuantity",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "QuantityForCartPro",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
