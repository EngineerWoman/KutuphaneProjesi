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
    public class CezaController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // HTTPGET ile cezaların listelenmesi
        public ActionResult Index()
        {
            var ceza = db.Ceza.Include(c => c.Hareket).Include(c => c.Uye);
            return View(ceza.ToList());
        }

        // HTTPGET ile cezaların ekleneceği sayfanın çağırılması
        public ActionResult Create()
        {
            ViewBag.HareketId = new SelectList(db.Hareket, "HareketId", "Not");
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad");
            return View();
        }

        // HTTPPOST ile cezaları eklenme işleminin yapılması 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CezaId,BaslangicTarih,BitisTarih,Para,UyeId,HareketId")] Ceza ceza)
        {
            if (ModelState.IsValid)
            {
                db.Ceza.Add(ceza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HareketId = new SelectList(db.Hareket, "HareketId", "Not", ceza.HareketId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad", ceza.UyeId);
            return View(ceza);
        }

        //HTTPGET ile cezaların güncelleneceği sayfanın çağırılması
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ceza ceza = db.Ceza.Find(id);
            if (ceza == null)
            {
                return HttpNotFound();
            }
            ViewBag.HareketId = new SelectList(db.Hareket, "HareketId", "Not", ceza.HareketId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad", ceza.UyeId);
            return View(ceza);
        }

        // HTTPGET ile cezaların güncelleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CezaId,BaslangicTarih,BitisTarih,Para,UyeId,HareketId")] Ceza ceza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ceza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HareketId = new SelectList(db.Hareket, "HareketId", "Not", ceza.HareketId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeAdSoyad", ceza.UyeId);
            return View(ceza);
        }
    }
}
