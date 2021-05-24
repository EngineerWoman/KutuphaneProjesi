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
    public class KategoriController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        public object KategoriAd { get; private set; }

        // httpget ile kitap ketegorilerinin listelenmesi
        public ActionResult Index()
        {
            return View(db.Kategori.ToList().OrderBy(x => x.KategoriSira));
        }
        //httpget ile kategori ekleme sayfasının getirilmesi
        public ActionResult KategoriEkle()
        {
            return View();
        }
        //Httpost ile kategori ekleme işeleminin yapılması
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            db.Kategori.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //httpget ile kategeri bilgilerinin güncelleneceği alanın getirilmesi
        public ActionResult KategoriGuncelle(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            var ktg = db.Kategori.Find(id);
            if (ktg==null)
            {
                return HttpNotFound();
            }
            return View(ktg);
        }
        //httppost ile kategori bilgilerinin güncelleme işleminin yapılması
        [HttpPost]
        public ActionResult KategoriGuncelle(int id, Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                var ktg = db.Kategori.Where(x => x.KategoriId == id).SingleOrDefault();
                ktg.KategoriAd= kategori.KategoriAd;
                ktg.KategoriSira = kategori.KategoriSira;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
           
        }
        //Kategori bilgilerinin silinmesi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            db.Kategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
