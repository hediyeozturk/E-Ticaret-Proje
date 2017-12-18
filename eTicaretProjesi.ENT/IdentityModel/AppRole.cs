using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.IdentityModel
{
    public class AppRole : IdentityRole
    {
        [StringLength(200)]
        public string RolAciklama { get; set; }
    }
}
