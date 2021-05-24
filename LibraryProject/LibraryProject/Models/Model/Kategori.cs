using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required, DisplayName("Sira")]
        public int KategoriSira { get; set; }
        [Required, DisplayName("Kategori Ad")]
        public string KategoriAd { get; set; }
        public ICollection<Kitap> Kitaps { get; set; }


    }
}