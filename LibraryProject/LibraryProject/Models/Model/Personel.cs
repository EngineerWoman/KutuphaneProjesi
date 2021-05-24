using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Personel")]
    public class Personel
    {
        [Key]
        public int PersoneId { get; set; }
        [Required, DisplayName("Sira")]
        public int PersonelSira { get; set; }
        [Required, DisplayName("Ad Soyad"),StringLength(100)]
        public string PersonelAdSoyad { get; set; }
        //public string KullaniciAdi { get; set; }
        //public string Sifre { get; set; }
        public ICollection<Hareket> Harekets { get; set; }
    }
}