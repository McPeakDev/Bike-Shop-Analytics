using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class ManufacturerTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManTraID { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public uint EmployeeID { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Reference { get; set; }

        //PRIMARY KEY (`MANUFACTURERID`,`TRANSACTIONDATE`,`REFERENCE`),
        //UNIQUE KEY `PK_MANUFTRANSACTION` (`MANUFACTURERID`,`TRANSACTIONDATE`,`REFERENCE`),
        //CONSTRAINT `FK_MANUFTRANS` FOREIGN KEY (`MANUFACTURERID`) REFERENCES `MANUFACTURER` (`MANUFACTURERID`) ON DELETE CASCADE
        //example data
        // (2,'1994-01-01 00:00:00',87295,250902.9000,'Initial Purchase for startup',2),
        // (2,'1994-01-31 00:00:00',87295,-250902.9000,'Automatic payment of bills',2),

        public override bool Equals(object obj)
        {
            if (obj is ManufacturerTransaction)
            {
                var objManTra = obj as ManufacturerTransaction;
                if (objManTra.ManTraID == ManTraID && objManTra.EmployeeID == EmployeeID)
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