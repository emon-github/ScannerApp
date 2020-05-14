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
        public string createUserId { get; set; }
        public string equipName { get; set; }
        public string isBind { get; set; }
        public string softVersion { get; set; }
        public string sn { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }
        public string imUserId { get; set; }
        public string imToken { get; set; }
        public string appId { get; set; }
        public string temperature { get; set; }
        public string deptId { get; set; }
        public string deptName { get; set; }
        public string companyName { get; set; }
        public string dataType { get; set; }
        public string validateTime { get; set; }
    }
}