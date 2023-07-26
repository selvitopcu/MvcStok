using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            //  var urunler = db.TblUrunler.Where(x=>x.DURUM==true).ToList();
            var urunler = db.TblUrunler.Where(x=>x.DURUM==true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.AD.Contains(p) && x.DURUM == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> urun = (from x in db.TblUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AD,
                                             Value = x.ID.ToString(),
                                         }).ToList();
            ViewBag.drop1 = urun;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TblUrunler t)
        {
            var ktgr = db.TblKategori.Where(x => x.ID == t.TblKategori.ID).FirstOrDefault();
            t.TblKategori = ktgr;
            db.TblUrunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.TblKategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.AD,
                                            Value = x.ID.ToString()
                                        }).ToList();
            var ktgr = db.TblUrunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }
        public ActionResult UrunGuncelle(TblUrunler p)
        {
            var urun = db.TblUrunler.Find(p.ID);
            urun.AD = p.AD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.SATISFIYAT = p.SATISFIYAT;
            urun.ALISFIYAT = p.ALISFIYAT;
            var ktg = db.TblKategori.Where(x => x.ID == p.TblKategori.ID).FirstOrDefault();
            urun.KATEGORI = ktg.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TblUrunler p)
        {
            var urunbul = db.TblUrunler.Find(p.ID);
            urunbul.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}