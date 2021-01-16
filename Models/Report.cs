using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AuthSystem.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string ReportedUserId { get; set; }
        public string Reporter { get; set; }
        public string ReportedItemId { get; set; }

        [Required]
        [StringLength(30)]
        public string Subject { get; set; }
    }
}
