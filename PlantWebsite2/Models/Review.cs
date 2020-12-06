using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlantWebsite.Models
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Required]
        [StringLength(10)]
        public string ReviewTitle { get; set; }
        [Required]
        [StringLength(100)]
        public string ReviewDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set;}
    }
}
