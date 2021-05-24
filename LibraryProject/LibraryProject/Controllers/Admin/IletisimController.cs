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
    public class IletisimController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // iletişim formundan gelen mesajların listelenemsi
        public ActionResult Index()
        {
            return View(db.Iletisim.ToList());
        }

        //httpget ile iletişim formundan gelen mesajların görüntüleneceği sayfanın getirilmesi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // httppost ileiletişim formundan gelen mesajların görüntüleme işleminin yapılması
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IletisimId,AdSoyad,Mail,Konu,Mesaj")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisim);
        }
        //iletişim formundan gelen mesajların silme işleminin yapılması
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            db.Iletisim.Remove(iletisim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
