using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Ceza")]
    public class Ceza
    {
        [Key]
        public int CezaId { get; set; }
        [Required, DisplayName("Başlangıç Tarihi")]
        public string BaslangicTarih { get; set; }
        [Required, DisplayName("Bitiş Tarihi")]
        public string BitisTarih { get; set; }
        [Required, DisplayName("Para")]
        public decimal Para { get; set; }
        public int? UyeId { get; set; }
        public Uye Uye { get; set; }
        public int? HareketId { get; set; }
        public Hareket Hareket { get; set; }

    }
}