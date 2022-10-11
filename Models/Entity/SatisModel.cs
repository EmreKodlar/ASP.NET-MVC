using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Stok_Proje.Models.Entity
{
    public class SatisModel : TBLSATISLAR
    {
        public List<TBLSATISLAR> SatisListe { get; set; }
        public List<TBLMUSTERILER> MusteriListe { get; set; }
        public List<TBLURUNLER> UrunListe { get; set; }

        public TBLMUSTERILER Musteriler { get; set; }
        public TBLURUNLER Urunler { get; set; }
        public TBLSATISLAR Satislar { get; set; }
       
    }
}