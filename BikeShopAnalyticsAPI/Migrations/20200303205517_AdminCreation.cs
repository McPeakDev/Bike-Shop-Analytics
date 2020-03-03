using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShopAnalyticsAPI.Migrations
{
    public partial class AdminCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndRange",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PlotItemFive",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlotItemFour",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlotItemThree",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartRange",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndRange",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemFive",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemFour",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PlotItemThree",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "StartRange",
                table: "Categories");
        }
    }
}
