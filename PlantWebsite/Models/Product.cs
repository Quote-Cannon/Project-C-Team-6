using System;
using System.ComponentModel.DataAnnotations;

namespace PlantWebsite.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Water { get; set; }
        public string Light { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
