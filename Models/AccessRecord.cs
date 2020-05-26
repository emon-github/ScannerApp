using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScannerApp.Models
{
    public class AccessRecord
    {
        [Key]
        public string id { get; set; }
        [Display(Name ="Time")]
        public DateTime recordTime { get; set; }
        public string passType { get; set; }
        [Display(Name = "Device")]
        public string sn { get; set; }
        [Display(Name = "Device Name")]
        public string equipName { get; set; }
        [Display(Name = "Photo")]
        public string photoUrl { get; set; }
        public string photoSize { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
        public string arType { get; set; }
        [Display(Name = "Temperature")]
        public string temperature { get; set; }
        public string deptId { get; set; }
        [Display(Name = "Dept")]
        public string deptName { get; set; }
        [Display(Name = "Card")]
        public string cardNum { get; set; }
    }
}