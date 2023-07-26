﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            var musteriliste =db.TblMusteri.Where(x=>x.DURUM==true).ToList().ToPagedList(sayfa,3);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteri p)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            p.DURUM = true;
            db.TblMusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(TblMusteri p)
        {
            var mustbul = db.TblMusteri.Find(p.ID);
            mustbul.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
           var mus=db.TblMusteri.Find(id);
            return View("MusteriGetir",mus);
        }
        public ActionResult MusteriGuncelle(TblMusteri t)
        {
            var mus = db.TblMusteri.Find(t.ID);
            mus.AD = t.AD;
            mus.SOYAD=t.SOYAD;
            mus.SEHIR=t.SEHIR;
            mus.BAKIYE=t.BAKIYE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
