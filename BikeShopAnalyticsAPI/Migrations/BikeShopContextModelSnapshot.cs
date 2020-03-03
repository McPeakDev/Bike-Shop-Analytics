﻿// <auto-generated />
using System;
using BikeShopAnalyticsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BikeShopAnalyticsAPI.Migrations
{
    [DbContext(typeof(BikeShopContext))]
    partial class BikeShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BikeShopAnalyticsAPI.Models.Entities.Bike", b =>
                {
                    b.Property<int>("BikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalesPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BikeID");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("BikeShopAnalyticsAPI.Models.Entities.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChartType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndRange")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlotItemFive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlotItemFour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlotItemOne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlotItemThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlotItemTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartRange")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BikeShopAnalyticsAPI.Models.Entities.SalesOrder", b =>
                {
                    b.Property<int>("SalesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BikeID")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalesID");

                    b.HasIndex("BikeID");

                    b.ToTable("SalesOrders");
                });

            modelBuilder.Entity("BikeShopAnalyticsAPI.Models.Entities.SalesOrder", b =>
                {
                    b.HasOne("BikeShopAnalyticsAPI.Models.Entities.Bike", "Bike")
                        .WithMany()
                        .HasForeignKey("BikeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
