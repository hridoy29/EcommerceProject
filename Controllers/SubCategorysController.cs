using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_497.Models;

namespace Project_497.Controllers
{
    public class SubCategorysController : Controller
    {
        private projectEntities3 db = new projectEntities3();

        // GET: SubCategorys
        public ActionResult Index()
        {
            var subCategoryIDs = db.SubCategoryIDs.Include(s => s.category);
            return View(subCategoryIDs.ToList());
        }

        // GET: SubCategorys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryID subCategoryID = db.SubCategoryIDs.Find(id);
            if (subCategoryID == null)
            {
                return HttpNotFound();
            }
            return View(subCategoryID);
        }

        // GET: SubCategorys/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.categories, "categoryId", "name");
            return View();
        }

        // POST: SubCategorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryID1,name,categoryid")] SubCategoryID subCategoryID)
        {
            if (ModelState.IsValid)
            {
                db.SubCategoryIDs.Add(subCategoryID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.categories, "categoryId", "name", subCategoryID.categoryid);
            return View(subCategoryID);
        }

        // GET: SubCategorys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryID subCategoryID = db.SubCategoryIDs.Find(id);
            if (subCategoryID == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.categories, "categoryId", "name", subCategoryID.categoryid);
            return View(subCategoryID);
        }

        // POST: SubCategorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryID1,name,categoryid")] SubCategoryID subCategoryID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategoryID).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.categories, "categoryId", "name", subCategoryID.categoryid);
            return View(subCategoryID);
        }

        // GET: SubCategorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryID subCategoryID = db.SubCategoryIDs.Find(id);
            if (subCategoryID == null)
            {
                return HttpNotFound();
            }
            return View(subCategoryID);
        }

        // POST: SubCategorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoryID subCategoryID = db.SubCategoryIDs.Find(id);
            db.SubCategoryIDs.Remove(subCategoryID);
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
