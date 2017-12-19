using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using eTicaretProjesi.DAL;
using eTicaretProjesi.ENT.IdentityModel;

namespace eTicaretProjesi.BL.AccountRepository
{
    public class MemberShipTools
    {
        public static UserStore<AppUser> NewUserStore() => new UserStore<AppUser>(new MyContext());
        public static UserManager<AppUser> NewUserManager() => new UserManager<AppUser>(NewUserStore());

        public static RoleStore<AppRole> NewRoleStore() => new RoleStore<AppRole>(new MyContext());
        public static RoleManager<AppRole> NewRoleManager() => new RoleManager<AppRole>(NewRoleStore());
    }
}
