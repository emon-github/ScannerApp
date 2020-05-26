using PagedList;
using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScannerApp.Controllers
{
    public class UserListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserList
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<UserListViewModel> list = new List<UserListViewModel>();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneSortParm = String.IsNullOrEmpty(sortOrder) ? "phone_desc" : "";
            ViewBag.RoleSortParm = String.IsNullOrEmpty(sortOrder) ? "role_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            list = (from user in db.Users
                    from userRole in user.Roles
                    join role in db.Roles on
                    userRole.RoleId equals role.Id
                    select new UserListViewModel()
                    {
                        UserID = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        Role = role.Name
                    }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.UserName.ToLower().Contains(searchString)  
                                       || s.Phone.ToLower().Contains(searchString) 
                                       || s.Role.ToLower().Contains(searchString) ).ToList();
            }


            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(s => s.UserName).ToList();
                    break;
                case "phone_desc":
                    list = list.OrderBy(s => s.Phone).ToList();
                    break;
                case "role_desc":
                    list = list.OrderByDescending(s => s.Role).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.UserName).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));            
        }

        // GET: UserList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserList/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
