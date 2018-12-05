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
    public class PraceController : Controller
    {
        private Entities db = new Entities();

        // GET: Prace
        public ActionResult Index()
        {
            var prace = db.Prace.Include(p => p.Wydawcy);
            return View(prace.ToList());
        }

        // GET: Prace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prace prace = db.Prace.Find(id);
            if (prace == null)
            {
                return HttpNotFound();
            }
            return View(prace);
        }

        // GET: Prace/Create
        public ActionResult Create()
        {
            ViewBag.id_wydawcy = new SelectList(db.Wydawcy, "id_wydawcy", "nazwa");
            return View();
        }

        // POST: Prace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pracy,tytul,jezyk,rodzaj,rok_publikacji,slowa_kluczowe,id_wydawcy")] Prace prace)
        {
            if (ModelState.IsValid)
            {
                db.Prace.Add(prace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_wydawcy = new SelectList(db.Wydawcy, "id_wydawcy", "nazwa", prace.id_wydawcy);
            return View(prace);
        }

        // GET: Prace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prace prace = db.Prace.Find(id);
            if (prace == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_wydawcy = new SelectList(db.Wydawcy, "id_wydawcy", "nazwa", prace.id_wydawcy);
            return View(prace);
        }

        // POST: Prace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pracy,tytul,jezyk,rodzaj,rok_publikacji,slowa_kluczowe,id_wydawcy")] Prace prace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_wydawcy = new SelectList(db.Wydawcy, "id_wydawcy", "nazwa", prace.id_wydawcy);
            return View(prace);
        }

        // GET: Prace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prace prace = db.Prace.Find(id);
            if (prace == null)
            {
                return HttpNotFound();
            }
            return View(prace);
        }

        // POST: Prace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prace prace = db.Prace.Find(id);
            db.Prace.Remove(prace);
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
