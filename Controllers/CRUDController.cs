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
    public class CRUDController : Controller
    {
        private projectEntities3 db = new projectEntities3();

        // GET: CRUD
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Admin).Include(p => p.category).Include(p => p.SubCategoryID1);
            return View(products.ToList());
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Admins, "userId", "userName");
            ViewBag.categoryId = new SelectList(db.categories, "categoryId", "name");
            
            ViewBag.SubCategoryID = new SelectList(db.SubCategoryIDs, "SubCategoryID1", "name");
            return View();
        }

        // POST: CRUD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create( product product)
        {
            if (ModelState.IsValid)
            {
                product.click = 0;
                product.sellStartDate = DateTime.Now;
                product.clickDate = DateTime.Now.Date;
                product.sellEndDate = null;
                product.January = 0;
                product.February = 0;
                product.March = 0;
                product.April = 0;
                product.May = 0;
                product.June = 0;
                product.July = 0;
                product.August = 0;
                product.September = 0;
                product.October = 0;
                product.November = 0;
                product.December = 0;
                product.predection = 0;
                product.previous_click = 0;
                product.count_improvment = 0;
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Admins, "userId", "userName", product.userId);
            ViewBag.categoryId = new SelectList(db.categories, "categoryId", "name", product.categoryId);
           
            ViewBag.SubCategoryID = new SelectList(db.SubCategoryIDs, "SubCategoryID1", "name", product.SubCategoryID);
            return View(product);
        }

        // GET: CRUD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Admins, "userId", "userName", product.userId);
            ViewBag.categoryId = new SelectList(db.categories, "categoryId", "name", product.categoryId);
          
            ViewBag.SubCategoryID = new SelectList(db.SubCategoryIDs, "SubCategoryID1", "name", product.SubCategoryID);
            return View(product);
        }

        // POST: CRUD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(product product)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Admins, "userId", "userName", product.userId);
            ViewBag.categoryId = new SelectList(db.categories, "categoryId", "name", product.categoryId);
           
            ViewBag.SubCategoryID = new SelectList(db.SubCategoryIDs, "SubCategoryID1", "name", product.SubCategoryID);
            return View(product);
        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: CRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
