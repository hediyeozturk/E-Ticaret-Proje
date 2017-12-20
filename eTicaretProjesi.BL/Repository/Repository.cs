using eTicaretProjesi.ENT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.BL.Repository
{
    public class UrunlerRepo:BaseRepository<Urunler, int> { }

    public class KategoriRepo : BaseRepository<Kategoriler, int> { }

    public class AltKategoriRepo :BaseRepository<KategoriAlt, int> { }

}
