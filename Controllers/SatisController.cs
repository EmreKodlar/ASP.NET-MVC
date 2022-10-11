using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Stok_Proje.Models;
using MVC_Stok_Proje.Models.Entity;
//using PagedList; // jquery datatables ile yaptığımız için pagingE gerek duymadık...
//using PagedList.Mvc; // jquery datatables ile yaptığımız için pagingE gerek duymadık...

namespace MVC_Stok_Proje.Controllers
{
    public class SatisController : Controller
    {
        MvcDbStokEntities1 db = new MvcDbStokEntities1();

     
        public ActionResult SatisSirala()
        { 
            //dropdown için gerekli
            ViewBag.SatisUrun = new SelectList(db.TBLURUNLER.ToList(), "URUNID", "URUNAD");

            //dropdown için gerekli
            ViewBag.SatisMusteri = new SelectList(db.TBLMUSTERILER.ToList(), "MUSTERIID", "MUSTERIAD");

            SatisModel satis = new SatisModel();


            //Model de -> SatisModel  oluşturduk. İçerisine 3 tane class attık

            //Bu sayede hem tek model'den birçok sınıfa ulaşabildik...

            // sadece TBLSATISLAR içerisine public List  <TBLSATISLAR> SatisListe { get; set; } kodunu eklesek de olurdu.

            // buradaki amaç ilerleyen zamanlar için class birleştirmeyi öğrenmek...

            satis.SatisListe = db.TBLSATISLAR.ToList();
          
            satis.MusteriListe = db.TBLMUSTERILER.ToList();

            satis.UrunListe = db.TBLURUNLER.ToList();

            satis.Urunler = new TBLURUNLER();


            return View(satis);
        }
 
        public ActionResult YeniSatis(TBLSATISLAR p1)
        { 
            db.TBLSATISLAR.Add(p1);
            db.SaveChanges();

            //dropdown için gerekli
            ViewBag.SatisUrun = new SelectList(db.TBLURUNLER.ToList(), "URUNID", "URUNAD");

            //dropdown için gerekli
            ViewBag.SatisMusteri = new SelectList(db.TBLMUSTERILER.ToList(), "MUSTERIID", "MUSTERIAD");

            TempData["Mesaj"] = " Yeni Satış Eklendi...";

            return RedirectToAction("SatisSirala");
        }

        public ActionResult Sil(int id)
        {
            var deger= db.TBLSATISLAR.Find(id);
            db.TBLSATISLAR.Remove(deger);
            db.SaveChanges();
            TempData["Mesaj"] = " Satış Silindi...";
            return RedirectToAction("SatisSirala");
        }

         
        public ActionResult Guncelle(TBLSATISLAR p1) // Güncelleme butonuna basınca yapılacakalar
        {

            var deger = db.TBLSATISLAR.Find(p1.SATISID);

            deger.URUN = p1.URUN;
            deger.MUSTERI = p1.MUSTERI;
            deger.ADET = p1.ADET;
            deger.FIYAT = p1.FIYAT;

            db.SaveChanges();


            //dropdown için gerekli
            ViewBag.SatisUrun = new SelectList(db.TBLURUNLER.ToList(), "URUNID", "URUNAD", deger.URUN);
            // not : deger.URUN, seçili ürünün URUN id'si

            //dropdown için gerekli
            ViewBag.SatisMusteri = new SelectList(db.TBLMUSTERILER.ToList(), "MUSTERIID", "MUSTERIAD", deger.MUSTERI);
            // not : deger.MUSTERI, seçili ürünün MUSTERI id'si

            TempData["Mesaj"] = " Satış Düzenlendi...";

            return RedirectToAction("SatisSirala");
        }

        public ActionResult SatisDuzenle(int id) // sayfaya veri taşıma
        {
            var deger = db.TBLSATISLAR.Find(id);

            //dropdown için gerekli
            ViewBag.SatisUrun = new SelectList(db.TBLURUNLER.ToList(), "URUNID", "URUNAD", deger.URUN);
            // not : deger.URUN, seçili ürünün URUN id'si

            //dropdown için gerekli
            ViewBag.SatisMusteri = new SelectList(db.TBLMUSTERILER.ToList(), "MUSTERIID", "MUSTERIAD", deger.MUSTERI);
            // not : deger.MUSTERI, seçili ürünün MUSTERI id'si

            return View(deger);

        }


    }
}