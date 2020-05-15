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
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
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

    }
}