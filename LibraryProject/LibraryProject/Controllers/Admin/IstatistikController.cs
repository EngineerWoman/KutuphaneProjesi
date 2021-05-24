using LibraryProject.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers.Admin
{
    public class IstatistikController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        // GET: Istatistik
        public ActionResult Index()
        {
            ViewBag.Uye = db.Uye.Count();
            ViewBag.Yazar = db.Yazar.Count();
            ViewBag.Kitap = db.Kitap.Count();
            ViewBag.Kasa = db.Ceza.Sum(x => x.Para);
            return View();
        }
        public ActionResult LinqKart()
        {
            ViewBag.Uye = db.Uye.Count();
            ViewBag.Yazar = db.Yazar.Count();
            ViewBag.Kitap = db.Kitap.Count();
            ViewBag.Kasa = db.Ceza.Sum(x => x.Para);
            ViewBag.EmanetKitap = db.Kitap.Where(x=>x.Durum==false).Count();
            ViewBag.Kategori = db.Kategori.Count();
            ViewBag.Personel = db.Personel.Count();
            ViewBag.Mesaj = db.Iletisim.Count();
            
            return View();
        }
    }
}