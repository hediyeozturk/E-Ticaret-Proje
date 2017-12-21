using eTicaretProjesi.ENT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.BL.Repository
{
    public class UrunlerRepo:BaseRepository<Urunler, int> { }

    public class KategoriRepo : BaseRepository<Kategoriler, int>
    {
        public List<KategoriAlt> AltKategoriList(int KatID)
        {
            return dbContext.KategoriAlt.Where(x=>x.KategoriID==KatID).ToList();
        }
    }
    public class AltKategoriRepo :BaseRepository<KategoriAlt, int> { }


}
