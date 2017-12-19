using eTicaretProjesi.ENT.IdentityModel;
using eTicaretProjesi.ENT.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.DAL
{
    public class MyContext : IdentityDbContext<AppUser>
    {
        public MyContext()
            : base("name=MyCon")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.RequireUniqueEmail = true;
        }

        public virtual DbSet<Urunler> Urunler { get; set; }
        public virtual DbSet<KategoriAlt> KategoriAlt { get; set; }
        public virtual DbSet<Kategoriler> Kategoriler { get; set; }
    }
}
