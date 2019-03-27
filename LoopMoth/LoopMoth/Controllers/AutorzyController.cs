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
    public class AutorzyController : Controller
    {
        private Entities db = new Entities();

        // GET: Autorzy
        public ActionResult Index()
        {
            return View(db.Autorzy.ToList());
        }

        // GET: Autorzy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorzy autorzy = db.Autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // GET: Autorzy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autorzy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_autora,imie")] Autorzy autorzy)
        {
            if (ModelState.IsValid)
            {
                db.Autorzy.Add(autorzy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autorzy);
        }

        // GET: Autorzy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorzy autorzy = db.Autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // POST: Autorzy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_autora,imie")] Autorzy autorzy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorzy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autorzy);
        }

        // GET: Autorzy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autorzy autorzy = db.Autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // POST: Autorzy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autorzy autorzy = db.Autorzy.Find(id);
            db.Autorzy.Remove(autorzy);
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
