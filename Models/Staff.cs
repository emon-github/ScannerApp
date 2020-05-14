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
        public string photoUrl { get; set; }
        public string deptName { get; set; }
        public string fiUrl { get; set; }
        public string phone { get; set; }
        public string imToken { get; set; }
        public string appId { get; set; }
        public string name { get; set; }
        public string imUserId { get; set; }
        public string deptId { get; set; }
        public string personType { get; set; }

    }
}