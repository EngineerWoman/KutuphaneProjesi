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
    public class FirmaBilgileriController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // httpget ile firma bilgilerinin listelenmesi
        public ActionResult Index()
        {
            return View(db.FirmaBilgileri.ToList());
        }

        // HTTPGET ile firma bilgilerinin güncelleneceği sayfanın getirilmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmaBilgileri firmaBilgileri = db.FirmaBilgileri.Find(id);
            if (firmaBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(firmaBilgileri);
        }

        // HTTTPPOST ile firma bilgilerinin güncelleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FirmaBilgileri firmaBilgileri, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var firmaBilgi = db.FirmaBilgileri.Where(x => x.FirmaId == id).SingleOrDefault();
                if (Resim != null)
                {
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo imgInfo = new FileInfo(Resim.FileName);
                    string firmaName = Resim.FileName + imgInfo.Extension;
                    img.Resize(400, 400);
                    img.Save("~/Content/Image/Firma/" + firmaName);
                    firmaBilgi.Resim = "/Content/Image/Firma/" + firmaName;
                }
                firmaBilgi.FirmaAdi = firmaBilgileri.FirmaAdi;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firmaBilgileri);
        }
    }
}
