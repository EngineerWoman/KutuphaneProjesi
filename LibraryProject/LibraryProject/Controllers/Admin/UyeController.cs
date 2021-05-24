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
    public class UyeController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // HttpGet ile üye bilgilerinin Listelenmesi
        public ActionResult Index()
        {
            return View(db.Uye.ToList());
        }
        //  HttpGet ile üye bilgilerinin Ekleneceği alanın Getirilmesi
        public ActionResult Create()
        {
            return View();
        }
        //Httppost ile üyenin bilgilerinin eklenemsi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Uye uye, HttpPostedFileBase Fotograf)
        {
            if (ModelState.IsValid)
            {
                if (Fotograf != null)
                {
                    WebImage img = new WebImage(Fotograf.InputStream);
                    FileInfo imgInfo = new FileInfo(Fotograf.FileName);
                    string uyeName = Fotograf.FileName + imgInfo.Extension;
                    //img.Resize(100, 70);
                    img.Save("~/Content/Image/Uye/" + uyeName);
                    uye.Fotograf = "/Content/Image/Uye/" + uyeName;
                }
                db.Uye.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uye);
        }

        //  HttpGet ile üye bilgilerinin güncellenmesi için ilgili alanın getirilmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        //  HttpPost ile üye bilgilerinin güncellenmesi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Uye uye, HttpPostedFileBase Fotograf)
        {
            if (ModelState.IsValid)
            {
                var u = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();
                if (Fotograf != null)
                {
                    WebImage img = new WebImage(Fotograf.InputStream);
                    FileInfo imgInfo = new FileInfo(Fotograf.FileName);
                    string uyeName = Fotograf.FileName + imgInfo.Extension;
                    //img.Resize(100, 70);
                    img.Save("~/Content/Image/Uye/" + uyeName);
                    u.Fotograf = "/Content/Image/Uye/" + uyeName;
                }
                u.UyeAdSoyad = uye.UyeAdSoyad;
                u.KullaniciAdi = uye.KullaniciAdi;
                u.Mail = uye.Mail;
                u.Sifre = uye.Sifre;
                u.UyeSira = uye.UyeSira;
                u.Adres = uye.Adres;
                u.Durum = uye.Durum;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uye);
        }
        //  HttpGet ile üye bilgilerinin Silinmesi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            db.Uye.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //HttpGet ile üyenin aldığı kitapların listelenmesi
        public ActionResult UyeKitapGecmis(int id)
        {
            ViewBag.Uye = db.Uye.Where(y=>y.UyeId==id).Select(z=>z.UyeAdSoyad).FirstOrDefault();
            var uye = db.Hareket.Include("Kitap").Include("Personel").Where(x => x.Uye.UyeId == id).ToList();
            return View(uye);
        }
     
    }
}
