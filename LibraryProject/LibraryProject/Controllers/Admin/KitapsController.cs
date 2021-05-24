using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;

namespace LibraryProject.Controllers.Admin
{
    public class KitapsController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // httpget ile kitapların listelenmesi
        public ActionResult Index()
        {
            var kitap = db.Kitap.Include(k => k.Kategori).Include(k => k.Yazar);
            return View(kitap.ToList());
        }
        // httpget ile kitap ekleme sayfasının getirilmesi
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd");
            ViewBag.YazarId = new SelectList(db.Yazar, "YazarId", "YazarAdSoyad");
            return View();
        }
        //Httppost ile kitap ekleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kitap kitap, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null)
                {
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgInfo = new FileInfo(Resim.FileName);
                    string kitapName = Resim.FileName + imgInfo.Extension;
                    img.Resize(400, 400);
                    img.Save("~/Content/Image/Kitap/" + kitapName);
                    kitap.Resim = "/Content/Image/Kitap/" + kitapName;
                }
                db.Kitap.Add(kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", kitap.KategoriId);
            ViewBag.YazarId = new SelectList(db.Yazar, "YazarId", "YazarAdSoyad", kitap.YazarId);
            return View(kitap);
        }
        //Httpget ile kitap bilgilerinin güncelleneceği sayfanın getirlmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", kitap.KategoriId);
            ViewBag.YazarId = new SelectList(db.Yazar, "YazarId", "YazarAdSoyad", kitap.YazarId);
            return View(kitap);
        }
        //httppost ile kitap bilgilerinin güncellenme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kitap kitap, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var kp = db.Kitap.Where(x => x.KitapId == id).SingleOrDefault();
                if (Resim != null)
                {

                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgInfo = new FileInfo(Resim.FileName);
                    string kitapName = Resim.FileName + imgInfo.Extension;
                    img.Resize(400, 400);
                    img.Save("~/Content/Image/Kitap/" + kitapName);
                    kp.Resim = "/Content/Image/Kitap/" + kitapName;

                }
                kp.KitapAd = kitap.KitapAd;
                kp.YayinEvi = kitap.YayinEvi;
                kp.KitapSira = kitap.KitapSira;
                kp.KitapKodu = kitap.KitapKodu;
                kp.Sayfa = kitap.Sayfa;
                kp.KategoriId = kitap.KategoriId;
                kp.YazarId = kitap.YazarId;
                kp.Durum = kitap.Durum;
                kp.Aciklama = kitap.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", kitap.KategoriId);
            ViewBag.YazarId = new SelectList(db.Yazar, "YazarId", "YazarAdSoyad", kitap.YazarId);
            return View(kitap);
        }

        // httpget ile kitap bilgilerinin silinmesi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            db.Kitap.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
