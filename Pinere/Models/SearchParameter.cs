using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinere.Models
{
    public class SearchParameter
    {
        public string Search { get; set; }
        public string NamaAirline { get; set; }
        public string NamaAirlineDis { get; set; }
        public string NomorPenerbangan { get; set; }
        public string WaktuBerangkat { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string KotaAsal { get; set; }
        public string KotaAsalDis { get; set; }
        public string KotaTujuan { get; set; }
        public string KotaTujuanDis { get; set; }
        public string TipeKotaTemp { get; set; }
        public string PenumpangSakit { get; set; }
        public List<SelectListItem> PenumpangSakitList { get; set; }
        public string RujukRS { get; set; }
        public List<SelectListItem> RujukRSList { get; set; }
        public string NamaPasien { get; set; }
        public string TanggalLahir { get; set; }
        public string RujukRSDis { get; set; }
        public string HasilDiagnosa { get; set; }
        public string Suspect { get; set; }
        public List<SelectListItem> SuspectList { get; set; }
        public string HasilLab { get; set; }
        public List<SelectListItem> HasilLabList { get; set; }
        public string Kondisi { get; set; }
        public List<SelectListItem> KondisiList { get; set; }

        public SearchParameter()
        {
            PenumpangSakitList = new List<SelectListItem>();
            PenumpangSakitList.Add(new SelectListItem { Value = "", Text = "-- Semua --" });
            PenumpangSakitList.Add(new SelectListItem { Value = "1", Text = "Ada" });
            PenumpangSakitList.Add(new SelectListItem { Value = "2", Text = "Tidak" });

            RujukRSList = new List<SelectListItem>();
            RujukRSList.Add(new SelectListItem { Value = "", Text = "-- Semua --" });
            RujukRSList.Add(new SelectListItem { Value = "1", Text = "Ya" });
            RujukRSList.Add(new SelectListItem { Value = "2", Text = "Tidak" });

            SuspectList = new List<SelectListItem>();
            SuspectList.Add(new SelectListItem { Value = "", Text = "-- Semua --" });
            SuspectList.Add(new SelectListItem { Value = "01EBL", Text = "Ebola" });
            SuspectList.Add(new SelectListItem { Value = "02MRS", Text = "Mers-CoV" });
            SuspectList.Add(new SelectListItem { Value = "03OTH", Text = "Lainnya" });

            HasilLabList = new List<SelectListItem>();
            HasilLabList.Add(new SelectListItem { Value = "", Text = "-- Semua --" });
            HasilLabList.Add(new SelectListItem { Value = "Positif", Text = "Positif" });
            HasilLabList.Add(new SelectListItem { Value = "Negatif", Text = "Negatif" });

            KondisiList = new List<SelectListItem>();
            KondisiList.Add(new SelectListItem { Value = "", Text = "-- Semua --" });
            KondisiList.Add(new SelectListItem { Value = "1", Text = "Sembuh" });
            KondisiList.Add(new SelectListItem { Value = "2", Text = "Pulang Paksa" });
            KondisiList.Add(new SelectListItem { Value = "3", Text = "Meninggal" });
        }
    }
}