using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryProject.Controllers.Admin
{
    public class AdminController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        //Admin ve kullanınıcı(Personel) Girş sayfasının çağırılması
        public ActionResult Login()
        {
            return View();
        }
        //Htppost ile Admin ve kullanınıcı(Personel) Girş işleminin yapılması
        [HttpPost]
        public ActionResult Login(User user)
        {
            var bilgiler = db.User.FirstOrDefault(x => x.Mail == user.Mail && x.Sifre == user.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();
                Session["Yetki"] = bilgiler.Yetki;
                return RedirectToAction("LinqKart", "Istatistik");
            }
            else
            {
                ViewBag.Uyari = "Email veya Şifreyi Hatalı Girdiniz. Lütfen Tekrar Deneyiniz!!!";
                return View();
            }
        }
        ////Admin ve kullanınıcı(Personel) Çıkış işlemi
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
        //Sisteme kayıtlı Admin ve kullanınıcı(Personel)ların listelenmesi
        public ActionResult Users()
        {
            return View(db.User.ToList().OrderBy(x => x.AdSoyad));
        }
        //Httpget ile kullanıcı ekleme sayfasının çağırılması
        public ActionResult KullaniciEkle()
        {
            return View();
        }
        //htppost ile kullanıcı ekleme işlemin yapılaması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciEkle(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(user);
        }
        //httpget ile kullanıcı bilgilerinin güncelleneceği sayfanınn çağırılması
        public ActionResult KullaniciGuncelle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var kullanici = db.User.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }
        //httppost le kullanıcı bilgilerinin güncellemesi 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciGuncelle(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var kullanici = db.User.Where(x => x.AdminId == id).SingleOrDefault();
                kullanici.AdSoyad = user.AdSoyad;
                kullanici.KullaniciAdi = user.KullaniciAdi;
                kullanici.Mail = user.Mail;
                kullanici.Sifre = user.Sifre;
                kullanici.Yetki = user.Yetki;
                db.SaveChanges();
                return RedirectToAction("Users");

            }
            return View(user);

        }
        //kullanıcı bilgilerinin silinmesi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var kullanici = db.User.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            db.User.Remove(kullanici);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}