﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AuthSystem.Models
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
        [Required]
        public string Picture { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Review> Reviews { get; set; }

        [Required]
        public string Password { get; set; }

        
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User()
        {
            UserDate = DateTime.UtcNow;
            Picture = File.ReadAllText(Directory.GetCurrentDirectory() + "/varbinoutput.txt");
        }

        /*public byte[] getImage()
        {
            string varbin = Picture;
            byte[] output = new byte[varbin.Length / 3];
            for (int i = 0; i < output.Length; i++)
            {
                int input = Convert.ToInt32(Convert.ToString(varbin[0]) + Convert.ToString(varbin[1]) + Convert.ToString(varbin[2]));
                output[i] = (byte)input;
                varbin = varbin.Remove(0, 3);
            }
            return output;
        }*/
    }
}