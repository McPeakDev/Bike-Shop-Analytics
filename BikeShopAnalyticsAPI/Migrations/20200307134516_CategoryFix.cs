using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class CategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlotItemFive",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemFour",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemThree",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlotItemFive",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlotItemFour",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlotItemThree",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
