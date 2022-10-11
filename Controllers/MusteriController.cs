using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Stok_Proje.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace MVC_Stok_Proje.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities1 db = new MvcDbStokEntities1();

        [HttpGet]
        public ActionResult MusteriSirala()
        {

            var deger = db.TBLMUSTERILER.ToList(); 

           // var deger = db.TBLMUSTERILER.ToList().ToPagedList(sayfa,4); // paging ile olan -> ToPagedList(başlangıç sayfa, her sayfada kaç tane olacağı)

            return View(deger);
        }

        [HttpPost]
        public ActionResult MusteriSirala(TBLMUSTERILER p1 )
        {
            //--reSİM eKLEME İŞLEMLERİ--- not: resim klasörünü View klasörü içine ekleME! yoksa çalışmıyor...

            if(Request.Files.Count>0) // resim dosyası seçili ise
            {
                String saniye= DateTime.Now.Second.ToString(); // sadece saniye
                String dakika = DateTime.Now.Minute.ToString(); // sadece dakika
                String saat = DateTime.Now.Hour.ToString(); // sadece saat
                String milisaniye = DateTime.Now.Hour.ToString(); // sadece milisaniye
                String gun  = DateTime.Now.Day.ToString(); // sadece gün
                String ay  = DateTime.Now.Month.ToString(); // sadece ay
                String yil  = DateTime.Now.Year.ToString(); // sadece yıl

                //String dosyaadi= Path.GetFileName(Request.Files[0].FileName);
                String dosyaadi = yil+ay+gun+saat+dakika+saniye+milisaniye;
                String dosyauzantisi = Path.GetExtension(Request.Files[0].FileName);
                String yol = "/Images/" + dosyaadi + dosyauzantisi;

                Request.Files[0].SaveAs(Server.MapPath(yol));// serverdaki dosyaya resmi kopyalama/kaydetme

                p1.MUSTERIFOTO= yol; // Veritabanına yolu kaydetme
            }

            //---------------------------


            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            TempData["Mesaj"] = " Yeni Müşteri Eklendi...";

            var deger = db.TBLMUSTERILER.ToList();

            return View(deger);
        }

        public ActionResult MusteriDuzenle(int id)
        {
            var degerler = db.TBLMUSTERILER.Find(id);

            return View(degerler);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var degerler = db.TBLMUSTERILER.Find(p1.MUSTERIID);

            degerler.MUSTERIAD = p1.MUSTERIAD;
            degerler.MUSTERISOYAD = p1.MUSTERISOYAD;

            db.SaveChanges();

            TempData["Mesaj"] = " Müşteri Düzenlendi...";

            return RedirectToAction("MusteriSirala");
        }

        public ActionResult FotoGuncelle(TBLMUSTERILER p1)
        {
            var degerler = db.TBLMUSTERILER.Find(p1.MUSTERIID);

            //---eski resmi dosyadan silme
            if (System.IO.File.Exists(Server.MapPath(degerler.MUSTERIFOTO)))
            {
                System.IO.File.Delete(Server.MapPath(degerler.MUSTERIFOTO));
            }
            //--------

            //--Yeniyi ekle
            //String dosyaadi = Path.GetFileName(Request.Files[0].FileName);

            String saniye = DateTime.Now.Second.ToString(); // sadece saniye
            String dakika = DateTime.Now.Minute.ToString(); // sadece dakika
            String saat = DateTime.Now.Hour.ToString(); // sadece saat
            String milisaniye = DateTime.Now.Hour.ToString(); // sadece milisaniye
            String gun = DateTime.Now.Day.ToString(); // sadece gün
            String ay = DateTime.Now.Month.ToString(); // sadece ay
            String yil = DateTime.Now.Year.ToString(); // sadece yıl

            String dosyaadi = yil + ay + gun + saat + dakika + saniye + milisaniye;

            String dosyauzantisi = Path.GetExtension(Request.Files[0].FileName);
            String yol = "/Images/" + dosyaadi + dosyauzantisi;
            Request.Files[0].SaveAs(Server.MapPath(yol));// serverdaki dosyaya resmi kopyalama/kaydetme
            degerler.MUSTERIFOTO = yol;
            //-------------------------- 

            db.SaveChanges();

            TempData["Mesaj"] = " Müşteri Fotoğrafı Düzenlendi...";

            return RedirectToAction("MusteriSirala");
        }

        public ActionResult Sil(int id)
        {
            var degerler = db.TBLMUSTERILER.Find(id);
            
            db.TBLMUSTERILER.Remove(degerler);

            //---resmi dosyadan silme
            if (System.IO.File.Exists(Server.MapPath(degerler.MUSTERIFOTO)))
            {
                System.IO.File.Delete(Server.MapPath(degerler.MUSTERIFOTO));
            }
            //-----


            db.SaveChanges();

            TempData["Mesaj"] = " Müşteri Silindi...";

            return RedirectToAction("MusteriSirala");
        }

    }
}