using LibraryProject.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers.Admin
{
    public class IslemController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();
        //httpget ile iade edilen kitapların listelenmesi
        public ActionResult Index()
        {
            return View(db.Hareket.Include("Kitap").Include("Personel").Include("Uye").Where(x => x.Durum == true).ToList().OrderBy(x => x.HareketSira));

        }
    }
}