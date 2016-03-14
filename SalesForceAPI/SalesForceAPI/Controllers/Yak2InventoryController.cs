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
    public class Yak2InventoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Yak2Inventory
        public ActionResult Index()
        {
            return View(db.Yak2Inventory.ToList());
        }

        // GET: Yak2Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yak2Inventory yak2Inventory = db.Yak2Inventory.Find(id);
            if (yak2Inventory == null)
            {
                return HttpNotFound();
            }
            return View(yak2Inventory);
        }

        // GET: Yak2Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yak2Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProductCode,Qty")] Yak2Inventory yak2Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Yak2Inventory.Add(yak2Inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yak2Inventory);
        }

        // GET: Yak2Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yak2Inventory yak2Inventory = db.Yak2Inventory.Find(id);
            if (yak2Inventory == null)
            {
                return HttpNotFound();
            }
            return View(yak2Inventory);
        }

        // POST: Yak2Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProductCode,Qty")] Yak2Inventory yak2Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yak2Inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yak2Inventory);
        }

        // GET: Yak2Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yak2Inventory yak2Inventory = db.Yak2Inventory.Find(id);
            if (yak2Inventory == null)
            {
                return HttpNotFound();
            }
            return View(yak2Inventory);
        }

        // POST: Yak2Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yak2Inventory yak2Inventory = db.Yak2Inventory.Find(id);
            db.Yak2Inventory.Remove(yak2Inventory);
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
