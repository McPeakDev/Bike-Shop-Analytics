﻿using System;
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

        public string CategoryName { get; set; }

        public string PlotItemOne { get; set; }

        public string PlotItemTwo { get; set; }

        public string PlotItemThree { get; set; }

        public string PlotItemFour { get; set; }

        public string PlotItemFive { get; set; }

        public string ChartType { get; set; }

        public DateTime StartRange { get; set; }

        public DateTime EndRange { get; set; }

    }
}
