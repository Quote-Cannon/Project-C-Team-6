﻿using System;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Required]
        [StringLength(50)]
       
        public string Name { get; set; }
        public string LatinName { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Water { get; set; }
        public string Light { get; set; }
        [DataType(DataType.Date)]
        public DateTime ProductDate { get; set; }
        [Required]
        public bool Trade { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public bool Post { get; set; }

        public Product()
        {
            ProductDate = DateTime.UtcNow;
            Picture = File.ReadAllText(Directory.GetCurrentDirectory() + "/varbinoutput.txt");
        }

    }
}