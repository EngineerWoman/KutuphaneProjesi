using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("FirmaBilgileri")]
    public class FirmaBilgileri
    {
        [Key]
        public int FirmaId { get; set; }
        [DisplayName("Firma Adı"), StringLength(300)]
        public string FirmaAdi { get; set; }
        [DisplayName("Logo")]
        public string Resim { get; set; }
    }
}