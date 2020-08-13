using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScannerApp.Models
{
    public class AccessRecord
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Key]
        public string id { get; set; }
        [Display(Name = "Time")]
        public DateTime recordTime { get; set; }
        public string passType { get; set; }
        [Display(Name = "Device")]
        public string sn { get; set; } 

        [Display(Name = "Device Name")]
        public string equipName { get; set; }
        [Display(Name = "Photo")]
        public string photoUrl { get; set; }
        public string basePhotoUrl { get { return "http://175.143.69.73:8085" + photoUrl; } }
        public string photoSize { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }       
        public string phone { get; set; }
        [Display(Name = "Phone")]
        public string MyPhone
        {
            get
            {
                return phone.Length > 11 ? phone.Substring(5) : phone;
            }
        }
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