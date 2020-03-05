using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShopAnalyticsAPI.Models.Entities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
            
        
    }
}
