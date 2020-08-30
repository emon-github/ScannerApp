using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ScannerApp.Models;

namespace ScannerApp.Controllers
{
    public class AccessRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessRecords
        public ActionResult Index(string currentFilter, string searchString, int? page, DateTime? dateFrom, DateTime? dateTo)
        {
            DateTime dtf = ViewBag.dtF = DateTime.Now;
            if (dateFrom != null)
            {
                dtf = ViewBag.dtF = Convert.ToDateTime(dateFrom);
            }

            ViewBag.dtT = DateTime.Now;
            DateTime dtt = DateTime.Now.AddDays(1);
            if (dateTo != null)
            {
                ViewBag.dtT = Convert.ToDateTime(dateTo);
                dtt = Convert.ToDateTime(dateTo).AddDays(1);
            }

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            List<AccessRecord> list = null;
            string client = User.Identity.Name;

            if (User.IsInRole("Admin"))
            {
                list = db.AccessRecords
                    .Where(_ => !string.IsNullOrEmpty(_.temperature))
                    .Where(_ => _.recordTime >= dtf.Date)
                    .Where(_ => _.recordTime <= dtt.Date)
                    .OrderByDescending(_ => _.recordTime).ToList();
            }
            else
            {

                list = (from d in db.Devices
                        join acc in db.AccessRecords on d.sn equals acc.sn
                        where d.client == User.Identity.Name && !string.IsNullOrEmpty(acc.temperature)
                        select acc).OrderByDescending(_ => _.recordTime).ToList();

                // list = db.AccessRecords.OrderByDescending(_ => _.recordTime).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.name.ToLower().Contains(searchString)
                                       || s.phone.ToLower().Contains(searchString)
                                       || s.sn.ToLower().Contains(searchString)
                                       || (s.deptName.ToLower().Contains(searchString) || string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(s.deptName))
                                       || s.equipName.ToLower().Contains(searchString)).ToList();
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));

            //  return View(db.AccessRecords.ToList());
        }

        // GET: AccessRecords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRecord accessRecord = db.AccessRecords.Find(id);
            if (accessRecord == null)
            {
                return HttpNotFound();
            }
            return View(accessRecord);
        }

        // GET: AccessRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,recordTime,passType,sn,equipName,photoUrl,photoSize,name,phone,arType,temperature,deptId,deptName,cardNum")] AccessRecord accessRecord)
        {
            if (ModelState.IsValid)
            {
                db.AccessRecords.Add(accessRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessRecord);
        }

        // GET: AccessRecords/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRecord accessRecord = db.AccessRecords.Find(id);
            if (accessRecord == null)
            {
                return HttpNotFound();
            }
            return View(accessRecord);
        }

        // POST: AccessRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,recordTime,passType,sn,equipName,photoUrl,photoSize,name,phone,arType,temperature,deptId,deptName,cardNum")] AccessRecord accessRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessRecord);
        }

        // GET: AccessRecords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRecord accessRecord = db.AccessRecords.Find(id);
            if (accessRecord == null)
            {
                return HttpNotFound();
            }
            return View(accessRecord);
        }

        // POST: AccessRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AccessRecord accessRecord = db.AccessRecords.Find(id);
            db.AccessRecords.Remove(accessRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
