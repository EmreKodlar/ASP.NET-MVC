using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Stok_Proje.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /*
        public ActionResult Hakkimizda()
        {
            ViewBag.Message = "Hakkımızda Sayfası";

            return View();
        }

        public ActionResult Iletisim()
        {
            ViewBag.Message = "İletişim Sayfası";

            return View();
        }

        public ActionResult Referanslar()
        {
            ViewBag.Message = "Referanslar Sayfası";
            return View();
        }
        */
    }
}