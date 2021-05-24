using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("Kasa")]
    public class Kasa
    {
        [Key]
        public int KasaId { get; set; }
        [Required,DisplayName("Ay")]
        public string Ay { get; set; }
        [Required, DisplayName("Tutar")]
        public decimal Tutar { get; set; }
    }
}