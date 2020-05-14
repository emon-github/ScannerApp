using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScannerApp.Controllers
{
    public class SelfregistrationController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "ID,Name,Phone,Gender")] Registration registration)
        {
            string name = registration.Name;
            int gender = registration.Gender;

            return RedirectToAction("UploadPhoto");
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("Notice");
        }

        public ActionResult Notice()
        {
            return View();
        }
    }
}