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
    public class IletisimBilgileriController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // iletişim bilgilerinin listelenemsi
        public ActionResult Index()
        {
            return View(db.IletisimBilgileri.ToList());
        }

        //httpget iletişim bilgilerinin güncelleneceği sayfanın getirilmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            IletisimBilgileri iletisimBilgileri = db.IletisimBilgileri.Find(id);
            if (iletisimBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(iletisimBilgileri);
        }

        // iletişim bilgilerinin güncelleneme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IletisimBilgileri iletisimBilgileri)
        {
            if (ModelState.IsValid)
            {
                var iletisim = db.IletisimBilgileri.Where(x => x.IletisimBilgiId == id).SingleOrDefault();
                iletisim.Telefon = iletisimBilgileri.Telefon;
                iletisim.Mail = iletisimBilgileri.Mail;
                iletisim.Adres = iletisimBilgileri.Adres;
                iletisim.Konum = iletisimBilgileri.Konum;
                iletisim.Facebook = iletisimBilgileri.Facebook;
                iletisim.Instagram = iletisimBilgileri.Instagram;
                iletisim.Linkedin = iletisimBilgileri.Linkedin;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisimBilgileri);
        }

        // iletişim bilgilerinin silinmesi işlemi

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IletisimBilgileri iletisimBilgileri = db.IletisimBilgileri.Find(id);
            if (iletisimBilgileri == null)
            {
                return HttpNotFound();
            }
            db.IletisimBilgileri.Remove(iletisimBilgileri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
