using eTicaretProjesi.BL.Repository;
using eTicaretProjesi.ENT.Model;
using eTicaretProjesi.ENT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static eTicaretProjesi.ENT.ViewModel.KategorilerViewModel;

namespace eTicaretProjesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IslemlerController : Controller
    {
        UrunlerRepo ur = new UrunlerRepo();
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
            var AltKategoriler = new List<SelectListItem>();
            akr.GenelListele().Where(z => z.KategoriID == id).ToList().ForEach(x => AltKategoriler.Add(new SelectListItem
            {
                Value = x.AltKategoriID.ToString(),
                Text = x.AltKategoriAD
            }));
            ViewBag.AltKDetay = AltKategoriler;
            return View(model);
        }

        //alt kategori işlemler
        AltKategoriRepo akr = new AltKategoriRepo();

        public ActionResult AltKategoriEkle()
        {
            var KategoriForDropDown = new List<SelectListItem>();
            kr.Listele().ForEach(x => KategoriForDropDown.Add(new SelectListItem
            {
                Text = x.KategoriAD,
                Value = x.KategoriID.ToString()
            }));
            ViewBag.KategoriDD = KategoriForDropDown;
            return View();
        }

        [HttpPost]
        public ActionResult AltKategoriEkle(AltKategoriViewModel model)
        {
            KategoriAlt kat = new KategoriAlt();
            kat.AltKategoriAD = model.AltKategoriAd;
            kat.KategoriID = model.Kategoriler.KategoriID;
            akr.Ekle(kat);
            return RedirectToAction("AltKategoriListele");
        }

        public ActionResult AltKategoriListele()
        {
            KategoriViewModel model = new KategoriViewModel();
            model.KategoriList = kr.Listele();
            return View(model);
        }

        public ActionResult AltKategoriGuncelle(int id)
        {
            KategoriViewModel model = new KategoriViewModel();
            model.Kategoriler = kr.IDyeGoreBul(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult AltKategoriGuncelle(KategoriViewModel model, int id)
        {
            Kategoriler kat = kr.IDyeGoreBul(id);
            kat.KategoriAD = model.Kategoriler.KategoriAD;
            kr.Guncelle();
            return RedirectToAction("KategoriListele");
        }

        public ActionResult AltKategoriSil(int id)
        {
            Kategoriler kat = kr.IDyeGoreBul(id);
            kr.Sil(kat);
            return RedirectToAction("KategoriListele");
        }

        public ActionResult AltKategoriDetay(int id, KategoriViewModel model)
        {
            model.Kategoriler = kr.IDyeGoreBul(id);
            return View(model);
        }

        public ActionResult UrunListele()
        {
            UrunlerViewModel model = new UrunlerViewModel();
            model.UrunlerList = ur.Listele();
            return View(model);


            //var model = ur.Listele();
            //return View(model);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UrunEkle(UrunlerViewModel model)
        {
            Urunler urun = new Urunler();
            urun.UrunAD = model.UrunAD;
            urun.UrunFiyat = model.UrunFiyat;
            urun.UrunTanitim = model.UrunTanitim;
            urun.UrunAciklama = model.UrunAciklama;
            urun.GununUrunu = model.GununUrunu;
            return RedirectToAction("UrunListele");
        }

    }
}