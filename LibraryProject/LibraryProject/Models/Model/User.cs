using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int AdminId { get; set; }
        [Required, DisplayName("Ad Soyad"), StringLength(50, ErrorMessage = "Lütfen Adınızı Tam Giriniz!!!")]
        public string AdSoyad { get; set; }
        [Required, DisplayName("Kullanıcı Adı"), StringLength(30, ErrorMessage = "Lütfen Kullanıcı Adınızı 30 Karakter Uzunluğunda Giriniz!!!")]
        public string KullaniciAdi { get; set; }
        [Required, DisplayName("Şifre"), StringLength(50, ErrorMessage = "Şifreniz 50 Karakter Uzunluğunda Olabilir")]
        public string Sifre { get; set; }
        [Required, DisplayName("Mail"), StringLength(50, ErrorMessage = "Lütfen Mail Adserinizi Doğru Giriniz!!!")]
        public string Mail { get; set; }
        [Required, DisplayName("Yetki"), StringLength(20, ErrorMessage = "Lütfen Yetkinizi  Seçiniz!!!")]
        public string Yetki { get; set; }
    }
}