using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Hareket")]
    public class Hareket
    {
        [Key]
        public int HareketId { get; set; }
        [DisplayName("Sıra")]
        public int HareketSira { get; set; }
        [DisplayName("Alış Tarihi")]
        public DateTime AlisTarih { get; set; }
        [DisplayName("İade Tarihi")]
        public DateTime IadeTarih { get; set; }
        [DisplayName("Not")]
        public string Not { get; set; }

        //[DisplayName("Üye Getirme Tarihi")]
        //public DateTime UyeGetirmeTarih { get; set; }
        [DisplayName("Durum")]
        public bool Durum { get; set; }
        public int? KitapId { get; set; }
        public Kitap Kitap { get; set; }
        public int? UyeId { get; set; }
        public Uye Uye { get; set; }
        public int? PersoneId { get; set; }
        public Personel Personel { get; set; }
        public ICollection<Ceza> Cezas { get; set; }
    }
}