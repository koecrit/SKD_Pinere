using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinere.Helper;

namespace Pinere.Models
{
    public class Sampel
    {
        public int Id { get; set; }
        public string PasienId { get; set; }
        public string TanggalAmbilSampel { get; set; }
        public string JenisSpesimen { get; set; }
        public string JenisPemeriksaanLab { get; set; }
        public string TanggalKirimSampel { get; set; }
        public string TanggalKeluarRS { get; set; }
        public string Kondisi { get; set; }
        public List<SelectListItem> KondisiList { get; set; }

        public Sampel()
        {
            KondisiList = new List<SelectListItem>();
            KondisiList.Add(new SelectListItem { Value = "", Text = "-- Silahkan Pilih --" });
            KondisiList.Add(new SelectListItem { Value = "1", Text = "Sembuh" });
            KondisiList.Add(new SelectListItem { Value = "2", Text = "Pulang Paksa" });
            KondisiList.Add(new SelectListItem { Value = "3", Text = "Meninggal" });
        }
    }
}