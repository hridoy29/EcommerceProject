using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;

namespace Project_497.Controllers
{
    public class RegistrationController : Controller
    {
        private projectEntities3 db = new projectEntities3();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin user)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(user);
        }


    }
}