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
    public class YazarController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // Yazarların Listelenmesi
        public ActionResult Index()
        {
            return View(db.Yazar.ToList().OrderBy(x => x.YazarSira));
        }
        //Yazar Ekleme Alanının Sayfasının HttpGEt ile getirilmesi
        public ActionResult YazarEkle()
        {
            return View();
        }
        //Httppost ile yazar ekleme işleminin dinamik hale getirilmesi
        [HttpPost]
        public ActionResult YazarEkle(Yazar yazar, HttpPostedFileBase Resim)
        {
            if (Resim != null)
            {

                WebImage img = new WebImage(Resim.InputStream);
                FileInfo imgInfo = new FileInfo(Resim.FileName);
                string yazarName = Resim.FileName + imgInfo.Extension;
                img.Resize(400, 400);
                img.Save("~/Content/Image/Yazar/" + yazarName);
                yazar.Resim = "/Content/Image/Yazar/" + yazarName;

            }
            db.Yazar.Add(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Yazar bilgilerinin HttpGet ile getirilmesi
        public ActionResult YazarGuncelle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var yzr = db.Yazar.Find(id);
            if (yzr == null)
            {
                return HttpNotFound();
            }
            return View(yzr);
        }
        //Htttppost ile yazar ile ilgili gelen alanların güncelleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YazarGuncelle(int id, Yazar yazar, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var yzr = db.Yazar.Where(x => x.YazarId == id).SingleOrDefault();
                if (Resim != null)
                {

                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgInfo = new FileInfo(Resim.FileName);
                    string yazarName = Resim.FileName + imgInfo.Extension;
                    img.Resize(400, 400);
                    img.Save("~/Content/Image/Yazar/" + yazarName);
                    yzr.Resim = "/Content/Image/Yazar/" + yazarName;

                }

                yzr.YazarAdSoyad = yazar.YazarAdSoyad;
                yzr.YazarSira = yazar.YazarSira;
                yzr.Aciklama = yazar.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yazar);

        }
        //Yazar Bilgilerinin Silinme Alanı
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Yazar yazar = db.Yazar.Find(id);
            if (yazar == null)
            {
                return HttpNotFound();
            }
            db.Yazar.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //İlgili Yazara Ait Kitapların Listelenme Alanı
        public ActionResult YazarKitaplar(int id)
        {
            ViewBag.Yazar = db.Yazar.Where(y => y.YazarId == id).Select(z => z.YazarAdSoyad).FirstOrDefault();
            var yazar = db.Kitap.Include("Kategori").Where(x => x.Yazar.YazarId == id).ToList();
            return View(yazar);
        }
    }
}
