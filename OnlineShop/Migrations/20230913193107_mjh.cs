using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    public partial class mjh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoritesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteID = table.Column<int>(type: "int", nullable: false),
                    prodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesProducts_Favorites_FavouriteID",
                        column: x => x.FavouriteID,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritesProducts_Products_prodID",
                        column: x => x.prodID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesProducts_FavouriteID",
                table: "FavoritesProducts",
                column: "FavouriteID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesProducts_prodID",
                table: "FavoritesProducts",
                column: "prodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritesProducts");

            migrationBuilder.DropTable(
                name: "Favorites");
        }
    }
}
