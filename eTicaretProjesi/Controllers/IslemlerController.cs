using eTicaretProjesi.BL.Repository;
using eTicaretProjesi.ENT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static eTicaretProjesi.ENT.ViewModel.KategorilerViewModel;

namespace eTicaretProjesi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class IslemlerController : Controller
    {
        KategoriRepo kr = new KategoriRepo();
        // GET: Islemler
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(KategoriViewModel model)
        {
            Kategoriler kat = new Kategoriler();
            kat.KategoriAD = model.KategoriAd;
            kr.Ekle(kat);
            return RedirectToAction("KategoriListele");
        }

        public ActionResult KategoriListele()
        {
            KategoriViewModel model = new KategoriViewModel();
            model.KategoriList = kr.Listele();
            return View(model);
        }

        public ActionResult KategoriGuncelle(int id)
        {
            KategoriViewModel model = new KategoriViewModel();
            model.Kategoriler = kr.IDyeGoreBul(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(KategoriViewModel model, int id)
        {
            Kategoriler kat = kr.IDyeGoreBul(id);
            kat.KategoriAD = model.Kategoriler.KategoriAD;
            kr.Guncelle();
            return RedirectToAction("KategoriListele");
        }

        public ActionResult KategoriSil(int id)
        {
            Kategoriler kat = kr.IDyeGoreBul(id);
            kr.Sil(kat);
            return RedirectToAction("KategoriListele");
        }

        public ActionResult KategoriDetay(int id, KategoriViewModel model)
        {
            model.Kategoriler = kr.IDyeGoreBul(id);
            return View(model);
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