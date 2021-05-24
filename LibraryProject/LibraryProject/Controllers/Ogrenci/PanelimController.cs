using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryProject.Controllers.Ogrenci
{
    [Authorize]
    public class PanelimController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        //Üye Bilgilerini Listeleme
        public ActionResult UyeBilgileriListele()
        {
            var uyeMail = (string)Session["Mail"];
            ViewBag.AdSoyad = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.UyeAdSoyad).FirstOrDefault();
            ViewBag.KullaniciAdi = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.KullaniciAdi).FirstOrDefault();
            ViewBag.Mail = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.Mail).FirstOrDefault();
            ViewBag.Telefon = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.Telefon).FirstOrDefault();
            ViewBag.Adres = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.Adres).FirstOrDefault();
            ViewBag.Fotograf = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.Fotograf).FirstOrDefault();
            var uyeId = db.Uye.Where(x => x.Mail == uyeMail).Select(z => z.UyeId).FirstOrDefault();
            ViewBag.KitapSayisi = db.Hareket.Where(x => x.UyeId == uyeId).Count();
            ViewBag.Duyuru = db.Duyuru.Count();

            return View(db.Uye.FirstOrDefault(z => z.Mail == uyeMail));
        }
        //HttpPost ta Üyenin Biilgilerinin Güncelleme İşlmeninin yapılması
        [HttpPost]
        public ActionResult UyeBilgileriListele(Uye uye, HttpPostedFileBase Fotograf)
        {
            if (ModelState.IsValid)
            {
                var kullanici = (string)Session["Mail"];
                var uyeBilgi = db.Uye.FirstOrDefault(x => x.Mail == kullanici);
                if (Fotograf != null)
                {
                    WebImage img = new WebImage(Fotograf.InputStream);
                    FileInfo imgInfo = new FileInfo(Fotograf.FileName);
                    string uyeName = Fotograf.FileName + imgInfo.Extension;
                    img.Resize(100, 70);
                    img.Save("~/Content/Image/Uye/" + uyeName);
                    uyeBilgi.Fotograf = "/Content/Image/Uye/" + uyeName;
                }
                uyeBilgi.Sifre = uye.Sifre;
                uyeBilgi.UyeAdSoyad = uye.UyeAdSoyad;
                uyeBilgi.KullaniciAdi = uye.KullaniciAdi;
                uyeBilgi.Adres = uye.Adres;
                uyeBilgi.Telefon = uye.Telefon;
                uyeBilgi.Mail = uye.Mail;
                db.SaveChanges();
                return RedirectToAction("UyeBilgileriListele");
            }
            return View(uye);

        }
        //Gelen Mesajları Listeleme
        public ActionResult GelenMesaj()
        {
            var gelenMesaj = (string)Session["Mail"].ToString();
            ViewBag.GelenMesaj = db.Mesaj.Where(x => x.Alici == gelenMesaj).Count();
            ViewBag.Duyuru = db.Duyuru.Count();
            return View(db.Mesaj.Where(x => x.Alici == gelenMesaj.ToString()).ToList().OrderByDescending(x => x.Tarih));
        }
        //Yeni Mesaj Sayfa Yapısının olduğu alan HttpGet ile gelir
        public ActionResult YeniMesaj()
        {
            var gelenMesaj = (string)Session["Mail"].ToString();
            ViewBag.GelenMesaj = db.Mesaj.Where(x => x.Alici == gelenMesaj).Count();
            ViewBag.Duyuru = db.Duyuru.Count();

            return View();
        }
        //HttpPost İle Yeni Mesaj İşlemi Dinamik Hale Getirildi Bu alanda
        [HttpPost]
        public ActionResult YeniMesaj(Mesaj mesaj)
        {
            var uyeMail = (string)Session["Mail"].ToString();
            mesaj.Gonderen = uyeMail.ToString();
            mesaj.Tarih = DateTime.Now;
            db.Mesaj.Add(mesaj);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj");
        }
        //Giden Mesajların Listelenmesi
        public ActionResult GidenMesaj()
        {
            ViewBag.Duyuru = db.Duyuru.Count();
            var gelenMesaj = (string)Session["Mail"].ToString();
            ViewBag.GelenMesaj = db.Mesaj.Where(x => x.Alici == gelenMesaj).Count();
            return View(db.Mesaj.Where(x => x.Gonderen == gelenMesaj.ToString()).ToList().OrderByDescending(x => x.Tarih));
        }
        //Gelen Mesajları Silme İşlemi
        public ActionResult GelenMesajSil(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var geleMmesajSil = db.Mesaj.Find(id);
            if (geleMmesajSil == null)
            {
                return HttpNotFound();
            }
            db.Mesaj.Remove(geleMmesajSil);
            db.SaveChanges();
            return RedirectToAction("GelenMesaj");
        }
        //Giden Mesajları Silme İşlemi
        public ActionResult GidenMesajSil(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var gidenMesajSil = db.Mesaj.Find(id);
            if (gidenMesajSil == null)
            {
                return HttpNotFound();
            }
            db.Mesaj.Remove(gidenMesajSil);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj");
        }
        //Üyenin Aldığı Kitapların Listelenme ile ilgili alanı
        public ActionResult Kitaplarim()
        {
            ViewBag.Duyuru = db.Duyuru.Count();
            var kullanici = (string)Session["Mail"].ToString();
            var id = db.Uye.Where(x => x.Mail == kullanici.ToString()).Select(z => z.UyeId).FirstOrDefault();
            var kitap = db.Hareket.Include("Kitap").Where(x => x.UyeId == id).ToList();
            return View(kitap);
        }
        //Yapılan duyurulanarın listelenmesi için gerekli alan
        public ActionResult Duyurular()
        {
            var duyuruListesi = db.Duyuru.ToList().OrderByDescending(x => x.Tarih);
            ViewBag.Duyuru = db.Duyuru.Count();
            return View(duyuruListesi);
        }
        //Öğrenci Panelinden Çıkış İşlemi
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}