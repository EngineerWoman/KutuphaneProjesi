using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;

namespace LibraryProject.Controllers.Admin
{
    public class HareketController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // ödünç verilen kitapların httpget ile listelenmesi
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var hareket = db.Hareket.Include(h => h.Kitap).Include(h => h.Uye).Include(h => h.Personel);
            return View(hareket.ToList().Where(x => x.Durum == false));
        }
        //Ödünç Kitap verme sayfasının httpget ile getirilmesi
        public ActionResult OduncKitapVerme()
        {
            ViewBag.KitapId = new SelectList(db.Kitap.Where(x => x.Durum == true), "KitapId", "KitapKodu");
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad");
            ViewBag.PersoneId = new SelectList(db.Personel, "PersoneId", "PersonelAdSoyad");
            return View();
        }
        //Ödünç kitap verme işleminin httppost ile yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult OduncKitapVerme(Hareket hareket)
        {
            db.Hareket.Add(hareket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // httpget ile iade sayfasının getirilmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hareket hareket = db.Hareket.Find(id);
            if (hareket == null)
            {
                return HttpNotFound();
            }
            ViewBag.KitapId = new SelectList(db.Kitap, "KitapId", "KitapKodu", hareket.KitapId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad", hareket.UyeId);
            ViewBag.PersoneId = new SelectList(db.Personel, "PersoneId", "PersonelAdSoyad", hareket.PersoneId);

            return View(hareket);
        }

        // httppost ile iade işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hareket hareket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hareket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KitapId = new SelectList(db.Kitap, "KitapId", "KitapKodu", hareket.KitapId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad", hareket.UyeId);
            ViewBag.PersoneId = new SelectList(db.Personel, "PersoneId", "PersonelAdSoyad", hareket.PersoneId);

            return View(hareket);
        }


    }
}
