using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Uye")]
    public class Uye
    {
        [Key]
        public int UyeId { get; set; }
        [DisplayName("Sıra")]
        public int UyeSira { get; set; }
        [DisplayName("Ad Soyad"), StringLength(100)]
        public string UyeAdSoyad { get; set; }
        [DisplayName("Kullanıcı Adı"), StringLength(100)]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre"), StringLength(10)]
        public string Sifre { get; set; }
        [DisplayName("Mail"), StringLength(100)]
        public string Mail { get; set; }
        [DisplayName("Telefon"), StringLength(20)]
        public string Telefon { get; set; }
        [DisplayName("Adres"), StringLength(2000)]
        public string Adres { get; set; }
        [DisplayName("Fotoğraf")]
        public string Fotograf { get; set; }
        [DisplayName("Durum")]
        public bool Durum { get; set; }
        public ICollection<Hareket> Harekets { get; set; }
        public ICollection<Ceza> Cezas { get; set; }

    }
}