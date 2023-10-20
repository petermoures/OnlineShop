using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    public partial class han : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderproduct_Products_productsId",
                table: "Orderproduct");

            migrationBuilder.RenameColumn(
                name: "productsId",
                table: "Orderproduct",
                newName: "producId");

            migrationBuilder.RenameIndex(
                name: "IX_Orderproduct_productsId",
                table: "Orderproduct",
                newName: "IX_Orderproduct_producId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Carts",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<int>(
                name: "QuantityForAddPro",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityForCartPro",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orderproduct_Products_producId",
                table: "Orderproduct",
                column: "producId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderproduct_Products_producId",
                table: "Orderproduct");

            migrationBuilder.DropColumn(
                name: "QuantityForAddPro",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuantityForCartPro",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "producId",
                table: "Orderproduct",
                newName: "productsId");

            migrationBuilder.RenameIndex(
                name: "IX_Orderproduct_producId",
                table: "Orderproduct",
                newName: "IX_Orderproduct_productsId");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Carts",
                newName: "Quantity");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderproduct_Products_productsId",
                table: "Orderproduct",
                column: "productsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
