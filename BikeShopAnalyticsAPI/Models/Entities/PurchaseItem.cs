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
        public int PurchaseItemID { get; set; }

        [Required]
        public uint ComponentID { get; set; }

        [Required]
        public int PricePaid { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int QuantityReceived { get; set; }

        // PRIMARY KEY (`PURCHASEID`,`COMPONENTID`),
        //UNIQUE KEY `PK_PURCHASEITEM` (`PURCHASEID`,`COMPONENTID`),
        //KEY `FK_REFERENCE21` (`COMPONENTID`),
        //CONSTRAINT `FK_REFERENCE20` FOREIGN KEY (`PURCHASEID`) REFERENCES `PURCHASEORDER` (`PURCHASEID`) ON DELETE CASCADE,
        //CONSTRAINT `FK_REFERENCE21` FOREIGN KEY (`COMPONENTID`) REFERENCES `COMPONENT` (`COMPONENTID`) ON DELETE CASCADE
        //example
        // (2,101000,49.4000,100,100),

        public override bool Equals(object obj)
        {
            if (obj is PurchaseItem)
            {
                var objPurchaseItem = obj as PurchaseItem;
                if (objPurchaseItem.PurchaseItemID == PurchaseItemID && objPurchaseItem.ComponentID == ComponentID)
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