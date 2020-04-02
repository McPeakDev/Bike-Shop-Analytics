using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models
{
    public class BikeShopContext : DbContext
    {
        public BikeShopContext(DbContextOptions<BikeShopContext> options) : base(options)
        {

        }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<ManufacturerTransaction> ManufacturerTransaction { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Auth> Auths { get; set; }

    }
}
