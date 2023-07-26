using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using System.Web.Security;
using System.Threading.Tasks;

namespace MvcStok.Controllers
{
    public class GirisController : Controller
    {
        // GET: Giris
        DbMvcStokEntities db=new DbMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(TblAdmin t)
        {
            var bilgiler=db.TblAdmin.FirstOrDefault(x=>x.KULLANICI==t.KULLANICI && x.SIFRE==t.SIFRE);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICI, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Giris", "Giris");
        }
    }
}