using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            var kategoriler=db.TblKategori.ToList();
            return View(kategoriler);
        }
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TblKategori p)
        {
            db.TblKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = db.TblKategori.Find(id);
            db.TblKategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TblKategori.Find(id);
            return View("KategoriGetir",ktg);
        }
        public ActionResult KategoriGuncelle(TblKategori p)
        {
            var kat = db.TblKategori.Find(p.ID);
            kat.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
 
    }
}