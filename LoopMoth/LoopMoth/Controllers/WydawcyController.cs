using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoopMoth.Models;

namespace LoopMoth.Controllers
{
    public class WydawcyController : Controller
    {
        private Entities db = new Entities();

        // GET: Wydawcy
        public ActionResult Index()
        {
            return View(db.Wydawcy.ToList());
        }

        // GET: Wydawcy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawcy wydawcy = db.Wydawcy.Find(id);
            if (wydawcy == null)
            {
                return HttpNotFound();
            }
            return View(wydawcy);
        }

        // GET: Wydawcy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wydawcy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nazwa,id_wydawcy")] Wydawcy wydawcy)
        {
            if (ModelState.IsValid)
            {
                db.Wydawcy.Add(wydawcy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wydawcy);
        }

        // GET: Wydawcy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawcy wydawcy = db.Wydawcy.Find(id);
            if (wydawcy == null)
            {
                return HttpNotFound();
            }
            return View(wydawcy);
        }

        // POST: Wydawcy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nazwa,id_wydawcy")] Wydawcy wydawcy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wydawcy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wydawcy);
        }

        // GET: Wydawcy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawcy wydawcy = db.Wydawcy.Find(id);
            if (wydawcy == null)
            {
                return HttpNotFound();
            }
            return View(wydawcy);
        }

        // POST: Wydawcy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wydawcy wydawcy = db.Wydawcy.Find(id);
            db.Wydawcy.Remove(wydawcy);
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
