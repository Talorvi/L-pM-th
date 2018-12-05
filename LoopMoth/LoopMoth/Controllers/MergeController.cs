using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoopMoth.Models
{
    public class MergeController : Controller
    {
        private Entities db = new Entities();
        // GET: Merge
        public ActionResult Index()
        {
            var list = new List<MergeViewModel>();
            var prace = db.Prace.Include(p => p.Wydawcy);
            foreach(var p in prace)
            {
                var mod = new MergeViewModel();
                mod.jezyk = p.jezyk;
                mod.rodzaj = p.rodzaj;
                mod.rok_publikacji = p.rok_publikacji;
                mod.slowa_kluczowe = p.slowa_kluczowe;
                mod.tytul = p.tytul;
                if(p.Wydawcy != null) mod.wydawca = p.Wydawcy.nazwa;
                var aut_list = new List<string>();
                foreach(var a in p.Autorzy)
                {
                    aut_list.Add(a.imie);
                }
                mod.autorzy = aut_list;
                var cat_list = new List<string>();
                foreach(var c in p.Kategorie)
                {
                    cat_list.Add(c.nazwa);
                }
                mod.kategorie = cat_list;
                list.Add(mod);
            }
            return View(list);
        }

        // GET: Merge/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Merge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Merge/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Merge/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Merge/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Merge/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Merge/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
