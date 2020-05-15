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
            string phone = registration.Phone;
            TempData["Employee"] = phone;

            return RedirectToAction("UploadPhoto");
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            string phone = "";

            if (TempData["Employee"] != null)
            {
                phone = TempData["Employee"].ToString();
            }

            string path = Server.MapPath("~/App_Data/uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            
            if (file != null)
            {
                var fileNameExt = Path.GetFileName(file.FileName);
                string[] ext = fileNameExt.ToString().Split('.');
                string name = string.IsNullOrEmpty(phone) ? ext[0] : phone;               
                file.SaveAs(Path.Combine(path, name + "." + ext[1]));
                return RedirectToAction("Notice");
            }
            else
            {
                ViewBag.Message = "Please Select any file";
                return View();
            }
        }

        public ActionResult Notice()
        {
            return View();
        }
    }
}