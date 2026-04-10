using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventory2026.Migrations
{
    /// <inheritdoc />
    public partial class ProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_products_codeInv",
                table: "products",
                newName: "IX_product_CodeInv");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Stock",
                table: "products",
                sql: "Stock >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Stock",
                table: "products");

            migrationBuilder.RenameIndex(
                name: "IX_product_CodeInv",
                table: "products",
                newName: "IX_products_codeInv");
        }
    }
}
