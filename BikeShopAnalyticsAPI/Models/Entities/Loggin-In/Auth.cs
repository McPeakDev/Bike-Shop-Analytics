using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities.Loggin_In
{
    public class Auth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthID { get; set; }

        [Required]
        public string Token { get; set; }

        [ForeignKey("Admin")]
        public int AdminID { get; set; }

        virtual public Admin Admin { get; set; }
    }
}
