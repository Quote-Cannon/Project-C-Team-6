using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Models;

namespace AuthSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string Nickname { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(6)")]
        public string PostCode { get; set; }

        public byte[] ProfilePicture { get; set; }

        public bool Banned { get; set; }
    }
}
