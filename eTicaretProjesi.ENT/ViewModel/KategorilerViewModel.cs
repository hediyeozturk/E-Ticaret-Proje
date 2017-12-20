using eTicaretProjesi.ENT.Model;
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

            public KategoriAlt KategoriAlt { get; set; }
            public IEnumerable<KategoriAlt> AltKategoriList { get; set; }
            public Kategoriler Kategoriler { get; set; }

        }

        public class KategoriViewModel
        {
            public int KategoriID { get; set; }
            public string KategoriAd { get; set; }

            public Kategoriler Kategoriler { get; set; }
            public IEnumerable<Kategoriler> KategoriList { get; set; }
            public KategoriAlt AltK { get; set; }
        }
    }
}
