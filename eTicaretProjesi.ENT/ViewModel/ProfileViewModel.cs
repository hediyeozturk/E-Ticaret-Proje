using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.ViewModel
{
    public class ProfilePasswordViewModel
    {
        public ProfileViewModel ProfileModel { get; set; } = new ProfileViewModel();
        public PasswordViewModel PasswordModel { get; set; } = new PasswordViewModel();
    }
    public class ProfileViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Ad")]
        [StringLength(25)]
        public string Ad { get; set; }
        [StringLength(35)]
        [Required]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAd { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class PasswordViewModel
    {
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır!")]
        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string EskiSifre { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır!")]
        public string YeniSifre { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("YeniSifre", ErrorMessage = "Şifreler uyuşmuyor")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır!")]
        public string SifreTekrar { get; set; }
    }

}
