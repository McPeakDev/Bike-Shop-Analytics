using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
        
        [Required]
        public string PlotItemOne { get; set; }

        [Required]
        public string PlotItemTwo { get; set; }

        [Required]
        public string ChartType { get; set; }

        [Required]
        public DateTime StartRange { get; set; }
        
        [Required]
        public DateTime EndRange { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                var objCat = obj as Category;
                if (objCat.CategoryID == CategoryID && objCat.CategoryName == CategoryName)
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
