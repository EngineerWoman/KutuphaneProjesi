using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LibraryProject.Models.Model;

namespace LibraryProject.Models.DataContext
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext() : base ("LibraryDB"){ }
        public DbSet<Kitap> Kitap { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Yazar> Yazar { get; set; }
        public DbSet<Uye> Uye { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Hareket> Hareket { get; set; }
        public DbSet<Ceza> Ceza { get; set; }
        public DbSet<Kasa> Kasa { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Mesaj> Mesaj { get; set; }
        public DbSet<Duyuru> Duyuru { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<IletisimBilgileri> IletisimBilgileri { get; set; }
        public DbSet<FirmaBilgileri> FirmaBilgileri { get; set; }

    }
}