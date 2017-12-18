using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.IdentityModel
{
    public class AppUser : IdentityUser
    {
        [StringLength(25)]
        public string Ad { get; set; }
        [StringLength(35)]
        public string KullaniciAdi { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
