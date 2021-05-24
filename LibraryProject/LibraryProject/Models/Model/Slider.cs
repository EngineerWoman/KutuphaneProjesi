using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Sıra")]
        public int Sira { get; set; }
        [DisplayName("Resim")]

        public string Resim { get; set; }
        [DisplayName("Başlık"), StringLength(100, ErrorMessage = "Açıklama Uzunluğu 100 Karakter Olmalıdır!!!")]

        public string Baslik { get; set; }
        [DisplayName("Açıklama"), StringLength(100, ErrorMessage ="Açıklama Uzunluğu 100 Karakter Olmalıdır!!!")]

        public string Aciklama { get; set; }
        [DisplayName("Durum")]
        public bool Durum { get; set; }
    }
}