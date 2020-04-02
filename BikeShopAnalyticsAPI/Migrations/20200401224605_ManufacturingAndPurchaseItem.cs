using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class ManufacturingAndPurchaseItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManufacturerTransaction",
                columns: table => new
                {
                    ManTraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    EMPLOYEEID = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Reference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerTransaction", x => x.ManTraID);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(nullable: false),
                    ManuID = table.Column<int>(nullable: false),
                    TotalList = table.Column<int>(nullable: false),
                    ShippingCost = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    AmountDue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManufacturerTransaction");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");
        }
    }
}
