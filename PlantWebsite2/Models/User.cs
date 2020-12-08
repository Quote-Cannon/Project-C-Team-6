using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlantWebsite.Models
{
    public class User
    {
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
        public bool Flagged { get; set; }
        [DataType(DataType.Date)]
        public DateTime UserDate { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public User()
        {
            Flagged = false;
        }
    }
}
