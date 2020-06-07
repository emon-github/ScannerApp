using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScannerApp.Models
{
    public class Device
    {
        [Key]
        public string id { get; set; }
        [ScaffoldColumn(false)]
        public string createUserId { get; set; }
        [Display(Name = "Device Name")]
        public string equipName { get; set; }
        [ScaffoldColumn(false)]
        public string isBind { get; set; }
        [ScaffoldColumn(false)]
        public string softVersion { get; set; }
        [Display(Name = "Device ID")]
        public string sn { get; set; }
        [ScaffoldColumn(false)]
        public string password { get; set; }
        [ScaffoldColumn(false)]
        public string ip { get; set; }
        [ScaffoldColumn(false)]
        public string status { get; set; }
        [ScaffoldColumn(false)]
        public string token { get; set; }
        [ScaffoldColumn(false)]
        public string createTime { get; set; }
        [ScaffoldColumn(false)]
        public string updateTime { get; set; }
        [ScaffoldColumn(false)]
        public string imUserId { get; set; }
        [ScaffoldColumn(false)]
        public string imToken { get; set; }
        [ScaffoldColumn(false)]
        public string appId { get; set; }
        [ScaffoldColumn(false)]
        public string temperature { get; set; }
        [ScaffoldColumn(false)]
        public string deptId { get; set; }
        [Display(Name = "Department")]
        public string deptName { get; set; }
        [Display(Name = "Company")]
        public string companyName { get; set; }
        [ScaffoldColumn(false)]
        public string dataType { get; set; }
        [ScaffoldColumn(false)]
        public string validateTime { get; set; }

        [Display(Name = "Client")]
        public string client { get; set; }

        [Display(Name = "Online")]
        public string onlineStatus { get; set; }


    }
}