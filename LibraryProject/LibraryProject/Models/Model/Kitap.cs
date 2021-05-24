using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Kitap")]
    public class Kitap
    {
        [Key]
        public int KitapId { get; set; }
        [ DisplayName("Sıra")]
        public int KitapSira { get; set; }
        [ DisplayName("Kitap Kodu"), StringLength(100)]
        public string KitapKodu { get; set; }
        [ DisplayName("Kitap Ad"), StringLength(400)]
        public string KitapAd { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        [ DisplayName("Resim")]
        public string Resim { get; set; }
        [DisplayName("Basım Yılı"),StringLength(10)]
        public string BasimYil { get; set; }
        [ DisplayName("Yayın Evi"),StringLength(300)]
        public string YayinEvi { get; set; }
        [ DisplayName("Sayfa"),StringLength(50)]
        public string Sayfa { get; set; }
        [DisplayName("Durum")]
        public bool Durum { get; set; }
        public int? KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public int? YazarId { get; set; }
        public Yazar Yazar { get; set; }
        public ICollection<Hareket> Harekets { get; set; }
    }
}