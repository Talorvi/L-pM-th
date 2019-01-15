using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoopMoth.Models;
using System.Web.Script.Serialization;

namespace LoopMoth.Controllers
{
    public class KategorieController : Controller
    {
        private Entities db = new Entities();

        private void Kat_List_Add(ref List<uKategoria> list, IQueryable<Kategorie> kategorie)
        {
            foreach (var it in kategorie)
            {
                var tmp = new uKategoria();
                tmp.nazwa = it.nazwa;
                tmp.id_kategorii = it.id_kategorii;
                var pk = db.Kategorie.Where(k => k.dziedzina == it.id_kategorii);
                if (pk.Count() > 0)
                {
                    var plist = new List<uKategoria>();
                    Kat_List_Add(ref plist, pk);
                    tmp.podkategorie = plist.ToArray();
                }
                list.Add(tmp);
            }
        }

        // GET: Kategorie/Create
        public ActionResult Create()
        {
            ViewBag.dziedzina = new SelectList(db.Kategorie, "id_kategorii", "nazwa");
            return PartialView();
        }

        public ActionResult _List(int id)
        {
            BibExport sex = new BibExport();
            ViewBag.Id = id;
            IQueryable<Kategorie> list;
            if (id == -1)
            {
                list = db.Kategorie.Where(k => k.dziedzina == null);
            }
            else
            {
                list = db.Kategorie.Where(k => k.dziedzina == id);
            }
            return PartialView(list);
        }

        public EmptyResult Clear()
        {
            return null;
        }

        // POST: Kategorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nazwa,id_kategorii,dziedzina")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Kategorie.Add(kategorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dziedzina = new SelectList(db.Kategorie, "id_kategorii", "nazwa", kategorie.dziedzina);
            return View(kategorie);
        }

        // GET: Kategorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorie kategorie = db.Kategorie.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            ViewBag.dziedzina = new SelectList(db.Kategorie, "id_kategorii", "nazwa", kategorie.dziedzina);
            return View(kategorie);
        }

        // POST: Kategorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nazwa,id_kategorii,dziedzina")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dziedzina = new SelectList(db.Kategorie, "id_kategorii", "nazwa", kategorie.dziedzina);
            return View(kategorie);
        }

        // GET: Kategorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorie kategorie = db.Kategorie.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorie kategorie = db.Kategorie.Find(id);
            db.Kategorie.Remove(kategorie);
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
