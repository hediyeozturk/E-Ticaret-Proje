using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.Model
{
    [Table("AltKategori")]
    public class KategoriAlt
    {
        [Key]
        public int AltKategoriID { get; set; }
        [Required(ErrorMessage = "Altkategori adı girmek zorunludur")]
        public string AltKategoriAD { get; set; }
        public int KategoriID { get; set; }
        [ForeignKey("KategoriID")]
        public virtual Kategoriler Kategori { get; set; }
    }
}
