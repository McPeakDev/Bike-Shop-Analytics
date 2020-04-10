using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class FixClassesMismatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchaseID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseID",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "Bikes");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderID",
                table: "PurchaseOrders",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseItemID",
                table: "PurchaseItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Bikes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "PurchaseOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                column: "PurchaseItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseItemID",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Bikes");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseID",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseID",
                table: "PurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "SalesPrice",
                table: "Bikes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "PurchaseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                column: "PurchaseID");
        }
    }
}
