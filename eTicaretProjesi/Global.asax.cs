using eTicaretProjesi.BL.AccountRepository;
using eTicaretProjesi.ENT.IdentityModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eTicaretProjesi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var roleManager = MemberShipTools.NewRoleManager();

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new AppRole()
                {
                    Name = "Admin",
                    RolAciklama = "Sistem yöneticisi"
                });
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new AppRole()
                {
                    Name = "User",
                    RolAciklama = "Sistem kullanıcısı"
                });
            }
            if (!roleManager.RoleExists("Passive"))
            {
                roleManager.Create(new AppRole()
                {
                    Name = "Passive",
                    RolAciklama = "E-Mail Aktivasyonu Gerekli"
                });
            }
        }
    }
}
