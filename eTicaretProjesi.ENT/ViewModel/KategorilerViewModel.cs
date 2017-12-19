using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.ViewModel
{
    public class KategorilerViewModel
    {
        public class AltKategoriViewModel
        {
            public int AltKategoriID { get; set; }
            public string AltKategoriAd { get; set; }
            public string KategoriAd { get; set; }
        }

        public class KategoriViewModel
        {
            public int KategoriID { get; set; }
            public string KategoriAd { get; set; }
        }
    }
}
