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
    public class LoginController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        // Login Sayfasının çağırılması
        public ActionResult Index()
        {
            return View();
        }
        //Httpget ile log,in sayfasının çağırılması
        public ActionResult GirisYap()
        {
            return View();
        }
        //httppost ile panele giriş yapılacağı alan
        [HttpPost]
        public ActionResult GirisYap(Uye uye)
        {
            var bilgiler = db.Uye.FirstOrDefault(x => x.Mail == uye.Mail && x.Sifre == uye.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["AdSoyad"] = bilgiler.UyeAdSoyad.ToString();
                Session["Mail"] = bilgiler.Mail.ToString();
                Session["KullaniciAdi"] = bilgiler.KullaniciAdi.ToString();
                Session["Sifre"] = bilgiler.Sifre.ToString();
                Session["Telefon"] = bilgiler.Telefon.ToString();
                //Session["Adres"] = bilgiler.Adres.ToString();
                //Session["Fotograf"] = bilgiler.Fotograf.ToString();
                return RedirectToAction("UyeBilgileriListele", "Panelim");
            }
            else
            {
                ViewBag.Uyari = "Email veya Şifreyi Hatalı Girdiniz. Lütfen Tekrar Deneyiniz!!!";
                return View();
            }

        }
        //Httpget ile öğrencinin(kişinin) kayıt olacağısayfanın getirilmesi
        public ActionResult KayitOl()
        {
            return View();
        }
        //Httppost ile kayıt alanının aktif hale gelmesi
        [HttpPost]
        public ActionResult KayitOl(Uye uye)
        {
            if (!ModelState.IsValid)
            {
                return View("KayitOl");
            }
            db.Uye.Add(uye);
            db.SaveChanges();
            return View();
        }
    }
}