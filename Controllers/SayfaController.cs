using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVC_Stok_Proje.Models.Entity;
using LazZiya.ImageResize;
using Image = System.Drawing.Image;

namespace MVC_Stok_Proje.Controllers
{
    public class SayfaController : Controller
    {
        MvcDbStokEntities1 db = new MvcDbStokEntities1();

        public ActionResult MenuPartial() // menu döndürme
        {
            var degerler = db.TBLSAYFALAR.ToList();
            
            return PartialView(degerler);
        }

         
        public ActionResult SayfaGoster(int id)
        {
            var degerler = db.TBLSAYFALAR.Find(id);
             
            return View(degerler);
        }

        
        public ActionResult SayfaDuzenle(int id)
        {
            var degerler = db.TBLSAYFALAR.Find(id);
 
            return View(degerler);
        }
       
        public ActionResult Guncelle(TBLSAYFALAR p1)
        {
            var degerler = db.TBLSAYFALAR.Find(p1.SAYFAID);

            degerler.SAYFAAD = p1.SAYFAAD;
            degerler.SAYFAYAZ = p1.SAYFAYAZ;
            db.SaveChanges();
            TempData["Mesaj"] = " Sayfa Düzenlendi...";
            return RedirectToAction("SayfaGoster", new { id = degerler.SAYFAID });
            //önemli: id değerini id olarak belirtmen lazım new { id = degerler.SAYFAID }
        }

        public ActionResult YeniSayfa(TBLSAYFALAR p1)
        {
            //--reSİM eKLEME İŞLEMLERİ--- not: resim klasörünü View klasörü içine ekleME! yoksa çalışmıyor...

            if (Request.Files.Count > 0) // resim dosyası seçili ise
            {
                String saniye = DateTime.Now.Second.ToString(); // sadece saniye
                String dakika = DateTime.Now.Minute.ToString(); // sadece dakika
                String saat = DateTime.Now.Hour.ToString(); // sadece saat
                String milisaniye = DateTime.Now.Hour.ToString(); // sadece milisaniye
                String gun = DateTime.Now.Day.ToString(); // sadece gün
                String ay = DateTime.Now.Month.ToString(); // sadece ay
                String yil = DateTime.Now.Year.ToString(); // sadece yıl

                //String dosyaadi= Path.GetFileName(Request.Files[0].FileName);
                String dosyaadi = yil + ay + gun + saat + dakika + saniye + milisaniye;
                String dosyauzantisi = Path.GetExtension(Request.Files[0].FileName);
                String yol = "/Images/" + dosyaadi + dosyauzantisi;

                Request.Files[0].SaveAs(Server.MapPath(yol));// serverdaki dosyaya resmi kopyalama/kaydetme
/*
                //----------resmi yeniden boyutlandırma------------------------------------

                //Şimdi ise bu kayıt ettiğimiz resmi Bitmap nesnesi şeklinde alıyoruz.
                Bitmap Resim = new Bitmap(Server.MapPath(yol));
               
                
                Bitmap BoyutlandirilmisResim = new Bitmap(Resim, 200,100); // boyutlarını verdik

                String yeniYol= "/Images/" +"merhaba"+ dosyaadi + dosyauzantisi; // yeniden isimlendirdik

                
                BoyutlandirilmisResim.Save(Server.MapPath(yeniYol), ImageFormat.Bmp); //kayıt ediyoruz.
                Resim.Dispose(); // Eskiyi Sil
                BoyutlandirilmisResim.Dispose();

                //Geçici olarak kaydedilen resmi siliyoruz.
                FileInfo IlkResimDosyasi = new FileInfo(Server.MapPath(yol));
                IlkResimDosyasi.Delete();

                daha iyi bir yöntem bul! çünkü gdi+ hatası geliyor...
*/


                //----------------------------------------


                p1.SAYFAFOTO = yol; // Veritabanına yolu kaydetme

            }

            //---------------------------


            db.TBLSAYFALAR.Add(p1);
            db.SaveChanges();
            TempData["Mesaj"] = " Sayfa Eklendi...";
            return RedirectToAction("SayfaGoster", new { id = p1.SAYFAID });
            
        }


        public ActionResult FotoDuzenle(TBLSAYFALAR p1)
        {
            var degerler = db.TBLSAYFALAR.Find(p1.SAYFAID);

            //---eski resmi dosyadan silme
            if (System.IO.File.Exists(Server.MapPath(degerler.SAYFAFOTO)))
            {
                System.IO.File.Delete(Server.MapPath(degerler.SAYFAFOTO));
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
            degerler.SAYFAFOTO = yol;
            //-------------------------- 

            db.SaveChanges();
            TempData["Mesaj"] = " Sayfa Düzenlendi...";
            return RedirectToAction("SayfaGoster", new { id = degerler.SAYFAID });
            //önemli: id değerini id olarak belirtmen lazım new { id = degerler.SAYFAID }
        }

        public ActionResult SayfaSil(TBLSAYFALAR P1)
        {
            int id=P1.SAYFAID;

            var SayfaSilModel = db.TBLSAYFALAR.Find(id);
            

            //---resmi dosyadan silme
            if (System.IO.File.Exists(Server.MapPath(SayfaSilModel.SAYFAFOTO)))
            {
                System.IO.File.Delete(Server.MapPath(SayfaSilModel.SAYFAFOTO));
            }
            //-----
            db.TBLSAYFALAR.Remove(SayfaSilModel);
            db.SaveChanges();
            TempData["Mesaj"] = " Sayfa Silindi...";
            return RedirectToAction("../Home/Index");

        }

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;

            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }



    }
} 