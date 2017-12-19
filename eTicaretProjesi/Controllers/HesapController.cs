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
using Microsoft.Owin.Security;

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


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userManager = MemberShipTools.NewUserManager();
            var user = await userManager.FindAsync(model.KullaniciAd, model.Sifre);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı bulunamadı");
                return View(model);
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, userIdentity);


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Hesap");
        }


        public async Task<ActionResult> Activation(string code)
        {
            var userStore = MemberShipTools.NewUserStore();
            var userManager = new UserManager<AppUser>(userStore);
            var sonuc = userStore.Context.Set<AppUser>().FirstOrDefault(x => x.ActivationCode == code);
            if (sonuc == null)
            {
                ViewBag.sonuc = "Aktivasyon işlemi  başarısız";
                return View();
            }
            sonuc.EmailConfirmed = true;
            await userStore.UpdateAsync(sonuc);
            await userStore.Context.SaveChangesAsync();

            userManager.RemoveFromRole(sonuc.Id, "Passive");
            userManager.AddToRole(sonuc.Id, "User");

            ViewBag.sonuc = $"Merhaba {sonuc.Ad} {sonuc.Soyad} <br/> Aktivasyon işleminiz başarılı";

            await SiteSettings.SendMail(new MailViewModel()
            {
                Kime = sonuc.Email,
                Mesaj = ViewBag.sonuc.ToString(),
                Konu = "Aktivasyon",
                Bcc = "cvtaydn53@gmail.com"
            });

            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(string email)
        {
            var userStore = MemberShipTools.NewUserStore();
            var userManager = new UserManager<AppUser>(userStore);
            var sonuc = userStore.Context.Set<AppUser>().FirstOrDefault(x => x.Email == email);
            if (sonuc == null)
            {
                ViewBag.sonuc = "E mail adresiniz sisteme kayıtlı değil";
                return View();
            }
            var randomPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            await userStore.SetPasswordHashAsync(sonuc, userManager.PasswordHasher.HashPassword(randomPass));
            await userStore.UpdateAsync(sonuc);
            await userStore.Context.SaveChangesAsync();

            await SiteSettings.SendMail(new MailViewModel()
            {
                Kime = sonuc.Email,
                Konu = "Şifreniz Değişti",
                Mesaj = $"Merhaba {sonuc.Ad} {sonuc.Soyad} <br/>Yeni Şifreniz : <b>{randomPass}</b>"
            });
            ViewBag.sonuc = "Email adresinize yeni şifreniz gönderilmiştir";
            return View();
        }

        [Authorize]
        public ActionResult Profil()
        {
            var userManager = MemberShipTools.NewUserManager();
            var user = userManager.FindById(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var model = new ProfilePasswordViewModel()
            {
                ProfileModel = new ProfileViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Ad = user.Ad,
                    Soyad = user.Soyad,
                    KullaniciAd = user.UserName
                }
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profil(ProfilePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var userStore = MemberShipTools.NewUserStore();
                var userManager = new UserManager<AppUser>(userStore);
                var user = userManager.FindById(model.ProfileModel.Id);
                user.Ad = model.ProfileModel.Ad;
                user.Soyad = model.ProfileModel.Soyad;
                if (user.Email != model.ProfileModel.Email)
                {
                    user.Email = model.ProfileModel.Email;
                    if (HttpContext.User.IsInRole("Admin"))
                    {
                        userManager.RemoveFromRole(user.Id, "Admin");
                    }
                    else if (HttpContext.User.IsInRole("User"))
                    {
                        userManager.RemoveFromRole(user.Id, "User");
                    }
                    userManager.AddToRole(user.Id, "Passive");
                    user.ActivationCode = Guid.NewGuid().ToString().Replace("-", "");
                    string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                                    (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    await SiteSettings.SendMail(new MailViewModel
                    {
                        Kime = user.Email,
                        Konu = "Personel Yönetimi - Aktivasyon",
                        Mesaj =
                                $"Merhaba {user.Ad} {user.Soyad} <br/>Email adresinizi <b>değiştirdiğiniz</b> için hesabınızı tekrar aktif etmelisiniz. <a href='{siteUrl}/Account/Activation?code={user.ActivationCode}'>Aktivasyon Kodu</a>"
                    });
                }
                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                var model1 = new ProfilePasswordViewModel()
                {
                    ProfileModel = new ProfileViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Ad = user.Ad,
                        Soyad = user.Soyad,
                        KullaniciAd = user.UserName
                    }
                };
                ViewBag.sonuc = "Bilgileriniz güncelleşmiştir";
                return View(model1);
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = ex.Message;
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(ProfilePasswordViewModel model)
        {
            if (model.PasswordModel.YeniSifre != model.PasswordModel.SifreTekrar)
            {
                ModelState.AddModelError(string.Empty, "Şifreler uyuşmuyor");
                return View("Profil", model);
            }
            try
            {
                var userStore = MemberShipTools.NewUserStore();
                var userManager = new UserManager<AppUser>(userStore);
                var user = userManager.FindById(model.ProfileModel.Id);
                user = userManager.Find(user.UserName, model.PasswordModel.EskiSifre);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Mevcut şifreniz yanlış girilmiştir");
                    return View("Profil", model);
                }
                await userStore.SetPasswordHashAsync(user, userManager.PasswordHasher.HashPassword(model.PasswordModel.YeniSifre));
                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Profil");
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Güncelleşme işleminde bir hata oluştu. " + ex.Message;
                return View("Profil", model);
            }
        }
    }
}