using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.ViewModel
{
    public class UrunlerViewModel
    {

        public int UrunID { get; set; }
        public string UrunAD { get; set; }
        public int AltKategoriID { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunAciklama { get; set; }
        public bool UrunTanitim { get; set; } = false;
        public bool GununUrunu { get; set; } = false;
        public string AltKategoriAd { get; set; }
    }
}
