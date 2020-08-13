using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScannerApp.Models
{
    public class Staff
    {
        [Key]
        public string perId { get; set; }
        [Display(Name="Photo")]
        public string photoUrl { get; set; }

        public string basePhotoUrl { get { return "http://175.143.69.73:8085" + photoUrl; } }
        [Display(Name = "Name")]
        public string name { get; set; }
        
        public string phone { get; set; }
        [Display(Name = "Phone")]
        public string MyPhone {
            get { 

                return phone.Length > 11? phone.Substring(5): phone; 
            } 
        }

        [Display(Name = "Department Name")]
        public string deptName { get; set; }
        [ScaffoldColumn(false)]
        public string fiUrl { get; set; }
       
        [ScaffoldColumn(false)]
        public string imToken { get; set; }
        [ScaffoldColumn(false)]
        public string appId { get; set; }
        
        [ScaffoldColumn(false)]
        public string imUserId { get; set; }
        [ScaffoldColumn(false)]
        public string deptId { get; set; }
        [ScaffoldColumn(false)]
        public string personType { get; set; }
        [Display(Name = "Client")]
        public string job { get; set; }
        public DateTime ORDER_BY_DERIVED_0 { get; set; }

        [Display(Name = "Reg. Device")]
        public string emerPer { get; set; }

    }
}