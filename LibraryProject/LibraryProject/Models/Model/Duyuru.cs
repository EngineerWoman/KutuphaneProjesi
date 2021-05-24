using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Duyuru")]
    public class Duyuru
    {
        [Key]
        public int DuyuruId { get; set; }
        [DisplayName("Sıra")]
        public int Sira { get; set; }
        [DisplayName("Başlık"),StringLength(50)]
        public string Baslik { get; set; }
        [DisplayName("İçerik"), StringLength(3000)]
        public string Icerik { get; set; }
        [DisplayName("Tarih")]
        public DateTime Tarih { get; set; }
        [DisplayName("Durum")]
        public bool Durum { get; set; }
    }
}