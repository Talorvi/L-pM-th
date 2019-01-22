using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            return View();
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
                if (TempData["path"] != null)
                {
                    db.Prace.Add(p);
                    db.SaveChanges();
                    var path = TempData["path"].ToString();
                    var file = new FileInfo(path);
                    path = Path.Combine(Server.MapPath("~/Content/Id/"), p.id_pracy.ToString()+".pdf");
                    file.CopyTo(path);
                }
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
        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Files/"), fileName);
                    System.Diagnostics.Debug.WriteLine(path);
                    file.SaveAs(path);
                    TempData["path"]=path;
                    var read = new WorkPiece();
                    var reader = new iTextSharp.text.pdf.PdfReader(path);
                    read.GetInfoFromPDF(reader, new FileInfo(path));
                    var model = new MergeViewModel(-1);
                    if(read.Author != null)
                    {
                        var auth = read.Author.Split(',').Select(a => a.Trim());
                        var base_auth = new List<Autorzy>();
                        foreach (var a in auth)
                        {
                            var tmp = new Autorzy();
                            tmp.imie = a;
                            base_auth.Add(tmp);
                        }
                        model.autorzy = base_auth;
                    }
                    if (read.Creator != null)
                    {
                        var wydawca = new Wydawcy();
                        wydawca.nazwa = read.Creator;
                        model.wydawca = wydawca;
                    }
                    if(read.Subject != null)
                    {
                        var cat = read.Subject.Split(',').Select(a => a.Trim());
                        var base_cat = new List<Kategorie>();
                        foreach (var a in cat)
                        {
                            var tmp = new Kategorie();
                            tmp.nazwa = a;
                            base_cat.Add(tmp);
                        }
                    }
                    model.rodzaj = read.Type;
                    model.jezyk = read.Language;
                    if(read.CreatedDate != null)
                        model.rok_publikacji = int.Parse(read.CreatedDate);
                    model.slowa_kluczowe = read.Keywords;
                    model.tytul = read.Title;
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult Delete()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int id = int.Parse(Request["id"]);
                    var item = db.Prace.Include("Kategorie").Include("Autorzy").Include("Wydawcy").SingleOrDefault(p => p.id_pracy == id);
                    var path = Path.Combine(Server.MapPath("~/Content/Id"), id.ToString() + ".pdf");
                    var file = new FileInfo(path);
                    file.Delete();
                    db.Prace.Remove(item);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
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
