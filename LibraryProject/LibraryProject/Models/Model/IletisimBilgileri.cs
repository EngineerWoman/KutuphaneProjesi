using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("IletisimBilgileri")]
    public class IletisimBilgileri
    {
        [Key]
        public int IletisimBilgiId { get; set; }
        [DisplayName("Telefon"), StringLength(13)]
        public string Telefon { get; set; }
        [DisplayName("Mail"), StringLength(100)]
        public string Mail { get; set; }
        public string Konum { get; set; }
        public string Adres { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
    }
}