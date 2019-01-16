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
        private int[] ids;
        // GET: Merge
        public ActionResult Index()
        {
            var list = new List<MergeViewModel>();
            var prace = db.Prace.Include(p => p.Wydawcy);
            foreach (var p in prace)
            {
                var mod = new MergeViewModel(p.id_pracy);
                mod.jezyk = p.jezyk;
                mod.rodzaj = p.rodzaj;
                mod.rok_publikacji = p.rok_publikacji;
                mod.slowa_kluczowe = p.slowa_kluczowe;
                mod.tytul = p.tytul;
                if (p.Wydawcy != null) mod.wydawca = p.Wydawcy;
                var aut_list = new List<Autorzy>();
                foreach (var a in p.Autorzy)
                {
                    aut_list.Add(a);
                }
                mod.autorzy = aut_list;
                var cat_list = new List<Kategorie>();
                foreach (var c in p.Kategorie)
                {
                    cat_list.Add(c);
                }
                mod.kategorie = cat_list;
                list.Add(mod);
            }
            return View(list);
        }

        public ActionResult Add()
        {
            return PartialView();
        }
        public JsonResult ConfirmAdd()
        {
            try
            {
                var p = new Prace();
                p.id_pracy = -1;
                p.jezyk = Request["lang"];
                p.rodzaj = Request["type"];
                if (Request["year"] != null && Request["year"].Length>0)
                {
                    var year = int.Parse(Request["year"]);
                    p.rok_publikacji = year;
                }
                else p.rok_publikacji = null;
                p.slowa_kluczowe = Request["keywords"];
                p.tytul = Request["title"];
                var nm = Request["publisher"];
                var publisher = db.Wydawcy.Where(w => w.nazwa == nm).ToList();
                if (publisher.Count() > 0)
                    p.Wydawcy = publisher.First();
                else
                {
                    if(nm == null)
                        p.Wydawcy = null;
                    else
                    {
                        var tmp = new Wydawcy();
                        tmp.nazwa = nm;
                        tmp.id_wydawcy = -1;
                        db.Wydawcy.Add(tmp);
                        p.Wydawcy = tmp;
                    }
                }
                var auth = Request["author"].Split(',').Select(a => a.Trim());
                var base_auth = db.Autorzy.Where(a => auth.Contains(a.imie)).ToList();
                foreach (var a in auth)
                {
                    if (base_auth.Where(b => b.imie == a).Count() < 1)
                    {
                        var tmp = new Autorzy();
                        tmp.imie = a;
                        tmp.id_autora = -1;
                        db.Autorzy.Add(tmp);
                        base_auth.Add(tmp);
                    }
                }
                p.Autorzy = base_auth;
                var cat = Request["cat"].Split(',').Select(a => a.Trim());
                var base_cat = db.Kategorie.Where(a => cat.Contains(a.nazwa)).ToList();
                p.Kategorie = base_cat;
                db.Prace.Add(p);
                db.SaveChanges();
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    int Id = (int)id;
                    var p = db.Prace.Find(id);
                    var mod = new MergeViewModel(Id);
                    mod.jezyk = p.jezyk;
                    mod.rodzaj = p.rodzaj;
                    mod.rok_publikacji = p.rok_publikacji;
                    mod.slowa_kluczowe = p.slowa_kluczowe;
                    mod.tytul = p.tytul;
                    if (p.Wydawcy != null) mod.wydawca = p.Wydawcy;
                    var aut_list = new List<Autorzy>();
                    foreach (var a in p.Autorzy)
                    {
                        aut_list.Add(a);
                    }
                    mod.autorzy = aut_list;
                    var cat_list = new List<Kategorie>();
                    foreach (var c in p.Kategorie)
                    {
                        cat_list.Add(c);
                    }
                    mod.kategorie = cat_list;
                    return View(mod);
                }

            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return View();
        }
        public JsonResult ConfirmEdit(int? id)
        {
            try
            {
                if (id != null)
                {
                    var p = db.Prace.Find(id);
                    p.jezyk = Request["lang"];
                    p.rodzaj = Request["type"];
                    if (Request["year"] != null && Request["year"].Length > 0)
                    {
                        var year = int.Parse(Request["year"]);
                        p.rok_publikacji = year;
                    }
                    else p.rok_publikacji = null;
                    p.slowa_kluczowe = Request["keywords"];
                    p.tytul = Request["title"];
                    var nm = Request["publisher"];
                    var publisher = db.Wydawcy.Where(w => w.nazwa == nm).ToList();
                    if (publisher.Count() > 0)
                        p.Wydawcy = publisher.First();
                    else
                    {
                        if (nm == null)
                            p.Wydawcy = null;
                        else
                        {
                            var tmp = new Wydawcy();
                            tmp.nazwa = nm;
                            tmp.id_wydawcy = -1;
                            db.Wydawcy.Add(tmp);
                            p.Wydawcy = tmp;
                        }
                    }
                    var auth = Request["author"].Split(',').Select(a => a.Trim());
                    var base_auth = db.Autorzy.Where(a => auth.Contains(a.imie)).ToList();
                    foreach (var a in auth)
                    {
                        if (base_auth.Where(b => b.imie == a).Count() < 1)
                        {
                            var tmp = new Autorzy();
                            tmp.imie = a;
                            tmp.id_autora = -1;
                            db.Autorzy.Add(tmp);
                            base_auth.Add(tmp);
                        }
                    }
                    p.Autorzy = base_auth;
                    var cat = Request["cat"].Split(',').Select(a => a.Trim());
                    var base_cat = db.Kategorie.Where(a => cat.Contains(a.nazwa)).ToList();
                    p.Kategorie = base_cat;
                    db.SaveChanges();
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int id = int.Parse(Request["id"]);
                    var item = db.Prace.Include("Kategorie").Include("Autorzy").Include("Wydawcy").SingleOrDefault(p => p.id_pracy == id);
                    db.Prace.Remove(item);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        public EmptyResult _Ids()
        {
            if (Request["ids"] != null)
            {
                ids = Request["ids"].Split(',').Select(x => { int value; bool success = int.TryParse(x, out value); return new { value, success }; }).Where(s => s.success).Select(s => s.value).ToArray();
            }
            return null;
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
