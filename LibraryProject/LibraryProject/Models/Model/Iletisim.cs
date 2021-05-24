using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        public int IletisimId { get; set; }
        [Required, DisplayName("Ad Soyad"), StringLength(300)]
        public string AdSoyad { get; set; }
        [Required, DisplayName("Mail"), StringLength(100)]
        public string Mail { get; set; }
        [Required, DisplayName("Konu"), StringLength(100)]
        public string Konu { get; set; }
        [Required, DisplayName("Mesaj")]
        public string Mesaj { get; set; }
    }
}