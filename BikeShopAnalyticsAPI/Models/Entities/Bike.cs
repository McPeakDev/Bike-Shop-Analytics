using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class Bike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BikeID { get; set; }

        public string Name { get; set; }

        public decimal SalesPrice { get; set; }

        /*  TODO: Implement
        public ModelType Type { get; set; }
        public Color Color { get; set; }
        */

        public override bool Equals(object obj)
        {
            if (obj is Bike)
            {
                var objBike = obj as Bike;
                if (objBike.BikeID == BikeID && objBike.Name == Name)
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
