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
    public class feed_backController : Controller
    {
        private projectEntities3 db = new projectEntities3();

        // GET: feed_back
        public ActionResult Index()
        {
            return View(db.feed_back.ToList());
        }

        // GET: feed_back/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed_back feed_back = db.feed_back.Find(id);
            if (feed_back == null)
            {
                return HttpNotFound();
            }
            return View(feed_back);
        }

        // GET: feed_back/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: feed_back/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "id,customer_Name,mail,message")] feed_back feed_back)
        {
            if (ModelState.IsValid)
            {
                db.feed_back.Add(feed_back);
                db.SaveChanges();
                return RedirectToAction("Index","home");
            }

            return View(feed_back);
        }

        // GET: feed_back/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed_back feed_back = db.feed_back.Find(id);
            if (feed_back == null)
            {
                return HttpNotFound();
            }
            return View(feed_back);
        }

        // POST: feed_back/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customer_Name,mail,message")] feed_back feed_back)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feed_back).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feed_back);
        }

        // GET: feed_back/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed_back feed_back = db.feed_back.Find(id);
            if (feed_back == null)
            {
                return HttpNotFound();
            }
            return View(feed_back);
        }

        // POST: feed_back/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            feed_back feed_back = db.feed_back.Find(id);
            db.feed_back.Remove(feed_back);
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
