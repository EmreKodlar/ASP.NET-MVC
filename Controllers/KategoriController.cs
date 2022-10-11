using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Stok_Proje.Models.Entity; // 1. eklenen
 

namespace MVC_Stok_Proje.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities1 db = new MvcDbStokEntities1(); // 2. eklenen

        /* bunları da KategoriSirala.cshtml'e ekleyeceksin
           
            @using MVC_Stok_Proje.Models.Entity 
            @model List<TBLKATEGORILER>
        */

        public ActionResult KategoriSirala()
        {
            var degerler = db.TBLKATEGORILER.ToList(); 
            
            return View(degerler);
        }

        [HttpGet] // sayfada post işlemi yapılmazsa, yani butona tklanmazsa
        public ActionResult YeniKategori() // YeniKategori sayfamıza gitmesi için gerekli
        {
            return View();
        }

        [HttpPost]// sayfada post işlemi yapılırsa, yani butona tklanırsa
        public ActionResult YeniKategori(TBLKATEGORILER p1)  
        {
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            TempData["Mesaj"] = " Yeni Kategori Eklendi...";
            return RedirectToAction("KategoriSirala");
        }

        public ActionResult Sil(int id) // Not: id'den başka bişey yazarsan (KAtID gibi) hata veriyor
        {
            var katSilModel=db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(katSilModel);
            db.SaveChanges();
            TempData["Mesaj"] = " Kategori Silindi...";
            return RedirectToAction("KategoriSirala");

        }

        public ActionResult KategoriDuzenle( int id ) // Güncelleme sayfasına ID taşı
        {
            var katModel = db.TBLKATEGORILER.Find(id);

            return View("KategoriDuzenle", katModel);

        }

        public ActionResult Guncelle(TBLKATEGORILER p1) // Güncelleme sayfasında butona tıklanınca yapılacaklar
        {
            var katModelGetir = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            katModelGetir.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            TempData["Mesaj"] = " Kategori Düzenlendi...";
            return RedirectToAction("KategoriSirala");

        }



    }
}