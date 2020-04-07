using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class FixCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlotItemOne",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemTwo",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "XCategory",
                table: "Categories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "XProperties",
                table: "Categories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YCategory",
                table: "Categories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YProperties",
                table: "Categories",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XCategory",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "XProperties",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "YCategory",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "YProperties",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "PlotItemOne",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlotItemTwo",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
