using LibraryProject.Models.DataContext;
using LibraryProject.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LibraryProject.Controllers.Admin
{
    public class GaleriController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        // Slider görsellerinin httpget ile listelenmesi
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }
        //httpget ile slider resim eklemesayfasının çağırılması
        public ActionResult Create()
        {
            return View();
        }
        //httppost ile slider resim ekleme ileminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider, HttpPostedFileBase Resim)
        {
            if (Resim != null)
            {
                WebImage img = new WebImage(Resim.InputStream);
                FileInfo imgınfo = new FileInfo(Resim.FileName);
                string galeriName = Resim.FileName + imgınfo.Extension;
                img.Save("~/Content/Image/Galeri/" + galeriName);
                slider.Resim = "/Content/Image/Galeri/" + galeriName;
            }
            db.Slider.Add(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //httpget ile slider güncelleme sayfasının çağırılması
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }
        //httpost ile slider güncelleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Slider slider, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var galeri = db.Slider.Where(x => x.SliderId == id).SingleOrDefault();
                if (Resim != null)
                {
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgınfo = new FileInfo(Resim.FileName);
                    string galeriName = Resim.FileName + imgınfo.Extension;
                    img.Save("~/Content/Image/Galeri/" + galeriName);
                    galeri.Resim = "/Content/Image/Galeri/" + galeriName;
                }
                galeri.Sira = slider.Sira;
                galeri.Baslik = slider.Baslik;
                galeri.Aciklama = slider.Aciklama;
                galeri.Durum = slider.Durum;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }
        //httpget ile silme işlemnin yapılması
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}