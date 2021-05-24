using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        //Anasayfa Alanı
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Galeri = db.Slider.ToList().OrderByDescending(x => x.Sira);
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();
            ViewBag.Kitap = db.Kitap.Include("Yazar").Include("Kategori").ToList().OrderBy(x => x.KitapSira);
            return View();
        }
       // Kitapların listelenme Sayfasının Alanı
        public ActionResult Books()
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();

            return View(db.Kitap.Include("Yazar").Include("Kategori").ToList().OrderBy(x => x.KitapSira));
        }
        //Kitap Detay Sayfası
        public ActionResult BookDetail(int id)
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();

            return View(db.Kitap.Include("Yazar").Include("Kategori").Where(x=>x.KitapId==id).SingleOrDefault());
        }
        //Yazarların Listelenme Sayfası
        public ActionResult Yazar()
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();

            return View(db.Yazar.ToList().OrderBy(x => x.YazarSira));
        }
        //Yazarların detaylarının görüntülenme sayfası
        public ActionResult YazarDetay(int id)
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();

            return View(db.Yazar.Where(x => x.YazarId == id).SingleOrDefault());
        }
        //Sliderın çekilme sayfası
        public ActionResult SliderPartial()
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();

            return View(db.Slider.Where(x=>x.Durum==true).ToList().OrderBy(x=>x.Sira));
        }
        //İletişim sayfası
        public ActionResult Contact()
        {
            ViewBag.Firma = db.FirmaBilgileri.FirstOrDefault();
            
            return View(db.IletisimBilgileri.FirstOrDefault());
        }
        //iletişim formuna girilen bilgilerin alanı
        [HttpPost]
        public ActionResult Contact(Iletisim iletisim)
        {
            db.Iletisim.Add(iletisim);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }

    }
}