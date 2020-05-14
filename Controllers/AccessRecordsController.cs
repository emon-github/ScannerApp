using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScannerApp.Models;

namespace ScannerApp.Controllers
{
    public class AccessRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessRecords
        public ActionResult Index()
        {
            return View(db.AccessRecords.ToList());
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
