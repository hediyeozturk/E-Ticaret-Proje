using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTicaretProjesi.ENT;
using System.Threading.Tasks;
using eTicaretProjesi.ENT.ViewModel;
using eTicaretProjesi.BL.AccountRepository;
using eTicaretProjesi.ENT.IdentityModel;
using eTicaretProjesi.BL.Settings;
using Microsoft.AspNet.Identity;

namespace eTicaretProjesi.Controllers
{
    public class HesapController : Controller
    {
        // GET: Hesap
        public ActionResult Uyelik()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Uyelik(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userManager = MemberShipTools.NewUserManager();
            var checkUser = userManager.FindByName(model.KullaniciAd);
            if (checkUser != null)
            {
                ModelState.AddModelError(string.Empty, "Bu kullanıcı zaten kayıtlı!");
                return View(model);
            }

            checkUser = userManager.FindByEmail(model.Email);
            if (checkUser != null)
            {
                ModelState.AddModelError(string.Empty, "Bu eposta adresi kullanılmakta");
                return View(model);
            }
            // register işlemi yapılır
            var activationCode = Guid.NewGuid().ToString();
            AppUser user = new AppUser()
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                UserName = model.KullaniciAd,
                ActivationCode = activationCode
            };

            var response = userManager.Create(user, model.Sifre);

            if (response.Succeeded)
            {
                string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                                 (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                if (userManager.Users.Count() == 1)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    await SiteSettings.SendMail(new MailViewModel
                    {
                        Kime = user.Email,
                        Konu = "Hoşgeldin Sahip",
                        Mesaj = "Sitemizi yöneteceğin için çok mutluyuz ^^"
                    });
                }
                else
                {
                    userManager.AddToRole(user.Id, "Passive");
                    await SiteSettings.SendMail(new MailViewModel
                    {
                        Kime = user.Email,
                        Konu = "Personel Yönetimi - Aktivasyon",
                        Mesaj =
                            $"Merhaba {user.Ad} {user.Soyad} <br/>Hesabınızı aktifleştirmek için <a href='{siteUrl}/Account/Activation?code={activationCode}'>Aktivasyon Kodu</a>"
                    });
                }

                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kayıt İşleminde bir hata oluştu");
                return View(model);
            }

        }

    }
}