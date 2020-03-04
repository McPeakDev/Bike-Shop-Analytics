using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Models.Entities.Loggin_In
{
    public class Credentials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CredentialID { get; set; }

        [Required]
        [StringLength(150)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 8)]
        public string Password { get; set; }

        [ForeignKey("Auth")]
        public int AuthID { get; set; }

        virtual public Auth Auth { get; set; }
    }
}
