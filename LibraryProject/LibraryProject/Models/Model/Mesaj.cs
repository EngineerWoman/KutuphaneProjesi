using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Mesaj")]
    public class Mesaj
    {
        [Key]
        public int MesajID { get; set; }
        [DisplayName("Gönderen")]
        public string Gonderen { get; set; }
        [DisplayName("Alıcı")]

        public string Alici { get; set; }
        [DisplayName("Konu")]
        public string Konu { get; set; }
        [DisplayName("İçerik")]

        public string Icerik { get; set; }
        [DisplayName("Tarih")]

        public DateTime Tarih { get; set; }
    }
}