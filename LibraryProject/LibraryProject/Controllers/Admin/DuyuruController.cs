using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers.Admin
{
    public class DuyuruController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        // HTTPGET ile duyuruların listelenmesi
        public ActionResult Index()
        {
            return View(db.Duyuru.ToList().OrderBy(x => x.Sira));
        }
       // HTTPGET ile duyuruların ekleneceği sayfanın çağırılması
        public ActionResult Create()
        {
            return View();
        }
        //HTTPPOST ile duyuruların ekleneme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Duyuru duyuru)
        {
            db.Duyuru.Add(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //HTTPGET ile duyuruların güncelleneceği sayfanın çağırılması
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var duyuru = db.Duyuru.Find(id);
            if (duyuru == null)
            {
                return HttpNotFound();
            }
            return View(duyuru);
        }
        //HTTPPOST ile duyuruların güncelleneceme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                var duyuruBildirimi = db.Duyuru.Where(x => x.DuyuruId == id).SingleOrDefault();
                duyuruBildirimi.Baslik = duyuru.Baslik;
                duyuruBildirimi.Sira = duyuru.Sira;
                duyuruBildirimi.Icerik = duyuru.Icerik;
                duyuruBildirimi.Durum = duyuru.Durum;
                duyuruBildirimi.Tarih = duyuru.Tarih;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //HTTPGET ile duyuruların silinmesi
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var duyuru = db.Duyuru.Find(id);
            if (duyuru == null)
            {
                return HttpNotFound();
            }
            db.Duyuru.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}