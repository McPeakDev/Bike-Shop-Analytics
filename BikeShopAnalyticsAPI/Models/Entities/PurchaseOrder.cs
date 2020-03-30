using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseID { get; set; }

        public int EmployeeID { get; set; }

        public int ManuID { get; set; }
        
        public int TotalList { get; set; }

        public int ShippingCost { get; set; }

        public int Discount { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ReceiveDate { get; set; }

        public int AmountDue { get; set; }

        //  PRIMARY KEY (`PURCHASEID`),
        // UNIQUE KEY `PK_PURCHASEORDER` (`PURCHASEID`),
        // KEY `FK_REFERENCE22` (`MANUFACTURERID`),
        // KEY `FK_REFERENCE23` (`EMPLOYEEID`),
        // CONSTRAINT `FK_REFERENCE22` FOREIGN KEY (`MANUFACTURERID`) REFERENCES `MANUFACTURER` (`MANUFACTURERID`) ON DELETE CASCADE,
        // CONSTRAINT `FK_REFERENCE23` FOREIGN KEY (`EMPLOYEEID`) REFERENCES `EMPLOYEE` (`EMPLOYEEID`) ON DELETE CASCADE
        //example 
        // (2,87295,2,286614.9000,90.0000,35802.0000,'1994-01-01 00:00:00','1994-01-01 00:00:00',250902.9000),
	    // (3,87295,3,109356.0000,75.0000,4407.0000,'1994-01-01 00:00:00','1994-01-01 00:00:00',105024.0000),
        //	(4,87295,4,13000.0000,55.0000,390.0000,'1994-01-01 00:00:00','1994-01-01 00:00:00',12665.0000),
    }
}