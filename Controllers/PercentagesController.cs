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
    public class PercentagesController : Controller
    {
        private projectEntities3 db = new projectEntities3();

        // GET: Percentages
        public ActionResult Index()
        {
            return View(db.Percentages.ToList());
        }

        // GET: Percentages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // GET: Percentages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Percentages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "percentageId,percentage1,date")] Percentage percentage)
        {
            if (ModelState.IsValid)
            {
                db.Percentages.Add(percentage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(percentage);
        }

        // GET: Percentages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // POST: Percentages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Percentage percentage)
        {
            if (ModelState.IsValid)
            {
                Click_Difference cd = new Click_Difference();
                var list = db.Click_Differences.Select(x => x.productId);
                foreach (var entity in db.Click_Differences.ToList())
                {
                    db.Click_Differences.Remove(entity);
                    db.SaveChanges();
                }
                db.SaveChanges();


                product p = new product();
                percentage.date = DateTime.Now.Date;
                db.Entry(percentage).State = EntityState.Modified;
                db.SaveChanges();
                //Percentage per = new Percentage();
                //var H = db.Percentages.Where(pr => pr.percentageId == 1).Select(x => x.date);
                //var temp = H.First();
                //var improve = db.products.Where(x=>x.clickDate==temp).OrderBy(x => x.productId).ToList();
                var improve = db.products.OrderBy(x => x.productId).ToList();

                foreach (var item in improve)
                {

                    Click_Difference c = new Click_Difference();
                    c.productId = item.productId;
                    c.ProductName = item.productName;
                    c.click = item.click;
                    c.Cdate = item.clickDate;
                    db.Click_Differences.Add(c);
                    db.SaveChanges();

                }
                

                return RedirectToAction("Index", "Product");
            }
            return View(percentage);
        }

        // GET: Percentages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // POST: Percentages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Percentage percentage = db.Percentages.Find(id);
            db.Percentages.Remove(percentage);
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
