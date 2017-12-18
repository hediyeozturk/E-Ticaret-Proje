using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.Model
{
    [Table("Urunler")]
    public class Urunler
    {
        [Key]
        public int UrunID { get; set; }
        [Required(ErrorMessage = "Ürün adı girilmelidir.")]
        [StringLength(25, ErrorMessage = "Ürün adı 5 ile 25 harf uzunluğunda olmalıdır.", MinimumLength = 5)]
        public string UrunAD { get; set; }
        public int AltKategoriID { get; set; }
        [Required(ErrorMessage = "Fiyat girmek zorunludur.")]
        public decimal UrunFiyat { get; set; }
        [Required(ErrorMessage = "Ürün açıklama girmek zorundasınız.")]
        public string UrunAciklama { get; set; }
        public bool UrunTanitim { get; set; } = false;
        public bool GununUrunu { get; set; } = false;
        [ForeignKey("AltKategoriID")]
        public virtual KategoriAlt Altkategori { get; set; }
    }
}
