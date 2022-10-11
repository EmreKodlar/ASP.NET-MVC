using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using MVC_Stok_Proje.Models.Entity;


namespace MVC_Stok_Proje.Controllers
{
    public class UrunController : Controller
    {

        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        
        public ActionResult UrunSirala()
        {
            var deger = db.TBLURUNLER.ToList(); 
            //var deger = db.TBLURUNLER.ToList().ToPagedList(sayfa, 4);
            return View(deger);
        }

        public ActionResult Sil(int id)
        {

            var silModel = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(silModel);
            db.SaveChanges();
            TempData["Mesaj"] = " Ürün Silindi...";
            return RedirectToAction("UrunSirala");
        }

        public ActionResult UrunDuzenle(int id) // sayfa geçişi
        {

            var duzModel = db.TBLURUNLER.Find(id); // id'si verilen ürünün tüm bilgileri alınıyor

            //dropdown için gerekli
            ViewBag.Urunler = new SelectList(db.TBLKATEGORILER.ToList(), "KATEGORIID", "KATEGORIAD", duzModel.URUNKATEGORI);
            // not : duzModel.URUNKATEGORI, seçili ürünün kategori id'si

            return View("UrunDuzenle", duzModel); // bilgileri taşıma işlemi...
        }

        public ActionResult Guncelle(TBLURUNLER p1) // güncelle butonuna basılışı
        {
            
            var duzModel = db.TBLURUNLER.Find(p1.URUNID);

            //dropdown için gerekli
            ViewBag.Urunler = new SelectList(db.TBLKATEGORILER.ToList(), "KATEGORIID", "KATEGORIAD", duzModel.URUNKATEGORI);
            // not : duzModel.URUNKATEGORI, seçili ürünün kategori id'si

            duzModel.URUNAD=p1.URUNAD;
            duzModel.URUNKATEGORI = p1.URUNKATEGORI;
            duzModel.FIYAT = p1.FIYAT;
            db.SaveChanges();
            TempData["Mesaj"] = " Ürün Düzenlendi...";
            return RedirectToAction("UrunSirala");

 
        }

        [HttpGet]
        public ActionResult YeniUrun()
        { 
            //dropdown için gerekli
            ViewBag.Urunler = new SelectList(db.TBLKATEGORILER.ToList(), "KATEGORIID", "KATEGORIAD");

            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            db.TBLURUNLER.Add(p1);
          
            db.SaveChanges();

            //dropdown için gerekli
            ViewBag.Urunler = new SelectList(db.TBLKATEGORILER.ToList(), "KATEGORIID", "KATEGORIAD");

            TempData["Mesaj"] = " Yeni Ürün Eklendi...";

            return RedirectToAction("UrunSirala");
        }

    }
}