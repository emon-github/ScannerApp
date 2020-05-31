using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScannerApp.Models
{
    public class Registration
    {
        public string ID { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Phone")]
        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }

        [Display(Name = "Gender")]        
       
        public int Gender { get; set; }
    }
}