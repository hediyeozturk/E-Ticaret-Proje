using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.Model
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        [Key]
        public int KategoriID { get; set; }
        [Required(ErrorMessage = "Kategori ad girmek zorunludur")]
        public string KategoriAD { get; set; }
    }
}
