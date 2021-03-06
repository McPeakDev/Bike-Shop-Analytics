﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesID { get; set; }

        [ForeignKey("Bike")]
        public int BikeID { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ShipDate { get; set; }
        
        [Required]
        public int StoreID { get; set; }

        [Required]
        public string State { get; set; }


        /*  TODO: Implement
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        
        [ForeignKey("Store")]
        public int StoreID { get; set; }

        */

        public virtual Bike Bike { get; set; }

        /*  TODO: Implement
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }
        */

        public override bool Equals(object obj)
        {
            if (obj is SalesOrder)
            {
                var objSalesOrder = obj as SalesOrder;
                if (objSalesOrder.SalesID == SalesID && objSalesOrder.BikeID == BikeID)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
