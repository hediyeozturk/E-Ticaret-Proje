using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eTicaretProjesi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class IslemlerController : Controller
    {
        // GET: Islemler
        public ActionResult KategoriEkle()
        {
            return View();
        }

        public ActionResult AltKategoriEkle()
        {
            return View();
        }

        public ActionResult UrunEkle()
        {
            return View();
        }

    }
}