using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var satislar = db.TblSatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from x in db.TblUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AD,
                                             Value = x.ID.ToString()
                                         }).ToList();
            ViewBag.drop1 = urun;

            List<SelectListItem> per = (from x in db.TblPersonel.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.AD + " "+ x.SOYAD,
                                            Value = x.ID.ToString()
                                        }).ToList();
            ViewBag.drop2 = per;

            List<SelectListItem> must = (from x in db.TblMusteri.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AD + " " + x.SOYAD,
                                             Value = x.ID.ToString()
                                         }).ToList();
            ViewBag.drop3 = must;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TblSatislar p)
        {
            var urun = db.TblUrunler.Where(x => x.ID == p.TblUrunler.ID).FirstOrDefault();
            var musteri = db.TblMusteri.Where(x => x.ID == p.TblMusteri.ID).FirstOrDefault();
            var personel = db.TblPersonel.Where(x => x.ID == p.TblPersonel.ID).FirstOrDefault();
            p.TblUrunler = urun;
            p.TblMusteri = musteri;
            p.TblPersonel=personel;
            p.TARIH =DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblSatislar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}