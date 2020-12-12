using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlantWebsite.Models
{
    public class User
    {

        public string[] flags { get; set; }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }
        public string MobileNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime UserDate { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Review> Reviews { get; set; }

        [Required]
        public string Password { get; set; }

        
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User()
        {

        }
    }
}
