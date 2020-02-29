using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class CategoryCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "Units",
                table: "SalesOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "ListPrice",
                table: "SalesOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "SalesOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShipDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StoreID",
                table: "SalesOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    PlotItemOne = table.Column<string>(nullable: true),
                    PlotItemTwo = table.Column<string>(nullable: true),
                    ChartType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "SalesOrders");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "SalesPrice",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Units",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
