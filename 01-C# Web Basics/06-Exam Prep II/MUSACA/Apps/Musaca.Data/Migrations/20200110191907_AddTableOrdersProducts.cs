using Microsoft.EntityFrameworkCore.Migrations;

namespace Musaca.Data.Migrations
{
    public partial class AddTableOrdersProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "OrdersProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrdersProducts",
                newName: "IX_OrdersProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersProducts_Orders_OrderId",
                table: "OrdersProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersProducts_Products_ProductId",
                table: "OrdersProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersProducts_Orders_OrderId",
                table: "OrdersProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersProducts_Products_ProductId",
                table: "OrdersProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts");

            migrationBuilder.RenameTable(
                name: "OrdersProducts",
                newName: "OrderProduct");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersProducts_ProductId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
