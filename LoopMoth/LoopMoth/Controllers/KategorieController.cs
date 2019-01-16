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

        public ActionResult _List()
        {
            ViewBag.Id = -1;
            IQueryable<Kategorie> list;
            if (Request["id"] != null)
            {
                var id = int.Parse(Request["id"]);
                ViewBag.Id = id;
                list = db.Kategorie.Where(k => k.dziedzina == id);
                
            }else list = db.Kategorie.Where(k => k.dziedzina == null);
            return PartialView(list);
        }

        public JsonResult Create()
        {
            try
            {
                if (Request["id"] != null)
                {
                    System.Diagnostics.Debug.WriteLine(Request["id"]);
                    var id = int.Parse(Request["id"]);
                    if (Request["name"] != null)
                    {
                        System.Diagnostics.Debug.WriteLine(Request["name"]);
                        var tmp = new Kategorie();
                        tmp.id_kategorii = -1;
                        tmp.nazwa = Request["name"];
                        if (id != -1) tmp.dziedzina = id;
                        db.Kategorie.Add(tmp);
                        db.SaveChanges();
                        var iid = tmp.id_kategorii;
                        return Json(new { result = true, id = iid }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit()
        {
            try
            {
                if (Request["id"] != null)
                {
                    var id = int.Parse(Request["id"]);
                    Kategorie kategorie = db.Kategorie.Find(id);
                    if (kategorie != null)
                    {
                        if (Request["name"] != null)
                        {
                            kategorie.nazwa = Request["name"];
                            db.SaveChanges();
                            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }catch(Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int id = int.Parse(Request["id"]);
                    var item = db.Kategorie.Include("Prace").SingleOrDefault(k => k.id_kategorii == id);
                    db.Kategorie.Remove(item);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception er) { System.Diagnostics.Debug.WriteLine(er.Message); }
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
