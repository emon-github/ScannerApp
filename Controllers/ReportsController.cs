using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScannerApp.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccessRecord()
        {
            return View();
        }
        public ActionResult PersonWise()
        {
            return View();
        }
    }
}