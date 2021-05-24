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
    public class PersonelController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        //  HttpGet ile personel bilgilerinin Listelenmesi
        public ActionResult Index()
        {
            return View(db.Personel.ToList().OrderBy(x => x.PersonelSira));
        }
        //HttpGet ile personel bilgilerinin Ekleneceği sayfanın getirilmesi
        public ActionResult PersonelEkle()
        {
            return View();
        }
        //HttpPost ile personel bilgilerinin Ekleneceği alan işleminin yapılması
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            db.Personel.Add(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //HttpGet ile personel bilgilerinin güncelleneceği sayfanın getirilmesi
        public ActionResult PersonelGuncelle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var prs = db.Personel.Find(id);
            if (prs == null)
            {
                return HttpNotFound();
            }
            return View(prs);
        }
        //HttpPost ile personel bilgilerinin güncelleme işleminin yapılması
        [HttpPost]
        public ActionResult PersonelGuncelle(int id, Personel personel)
        {
            if (ModelState.IsValid)
            {
                var prs = db.Personel.Where(x => x.PersoneId == id).SingleOrDefault();
                prs.PersonelAdSoyad = personel.PersonelAdSoyad;
                prs.PersonelSira = personel.PersonelSira;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personel);

        }
        //HttpGet ile personel bilgilerinin Silinesi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Personel personel = db.Personel.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
