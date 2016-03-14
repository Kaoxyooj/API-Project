using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesForceAPI.Models;

namespace SalesForceAPI.Controllers
{
    public class YakInventoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: YakInventories
        public ActionResult Index()
        {
            return View(db.YakInventories.ToList());
        }

        // GET: YakInventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YakInventory yakInventory = db.YakInventories.Find(id);
            if (yakInventory == null)
            {
                return HttpNotFound();
            }
            return View(yakInventory);
        }

        // GET: YakInventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YakInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProductCode,Qty,LocationId")] YakInventory yakInventory)
        {
            if (ModelState.IsValid)
            {
                db.YakInventories.Add(yakInventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yakInventory);
        }

        // GET: YakInventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YakInventory yakInventory = db.YakInventories.Find(id);
            if (yakInventory == null)
            {
                return HttpNotFound();
            }
            return View(yakInventory);
        }

        // POST: YakInventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProductCode,Qty,LocationId")] YakInventory yakInventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yakInventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yakInventory);
        }

        // GET: YakInventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YakInventory yakInventory = db.YakInventories.Find(id);
            if (yakInventory == null)
            {
                return HttpNotFound();
            }
            return View(yakInventory);
        }

        // POST: YakInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YakInventory yakInventory = db.YakInventories.Find(id);
            db.YakInventories.Remove(yakInventory);
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
