using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class PurchaseItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseID { get; set; }

        public int ComponentID { get; set; }

        public int PricePaid { get; set; }
        
        public int Quantity { get; set; }

        public int QuantityReceived { get; set; }

        // PRIMARY KEY (`PURCHASEID`,`COMPONENTID`),
        //UNIQUE KEY `PK_PURCHASEITEM` (`PURCHASEID`,`COMPONENTID`),
        //KEY `FK_REFERENCE21` (`COMPONENTID`),
        //CONSTRAINT `FK_REFERENCE20` FOREIGN KEY (`PURCHASEID`) REFERENCES `PURCHASEORDER` (`PURCHASEID`) ON DELETE CASCADE,
        //CONSTRAINT `FK_REFERENCE21` FOREIGN KEY (`COMPONENTID`) REFERENCES `COMPONENT` (`COMPONENTID`) ON DELETE CASCADE
        //example
        // (2,101000,49.4000,100,100),
    }
}