using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Yazar")]
    public class Yazar
    {
        [Key]
        public int YazarId { get; set; }
        [Required, DisplayName("Sıra")]
        public int YazarSira { get; set; }
        [Required, DisplayName("Ad Soyad"), StringLength(100)]
        public string YazarAdSoyad { get; set; }
        [DisplayName("Hakkında")]
        public string Aciklama { get; set; }
        [DisplayName("Resim")]
        public string Resim { get; set; }
        public ICollection<Kitap> Kitaps { get; set; }
    }
}