using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinere.Helper;

namespace Pinere.Models
{
    public class KKP
    {
        public int Id { get; set; }
        public string PasienId { get; set; }
        public string Suhu { get; set; }
        public string TekananDarah1 { get; set; }
        public string TekananDarah2 { get; set; }
        public string Pernapasan { get; set; }
        public string Nadi { get; set; }
        public string TandaGejala { get; set; }
        public bool VisitLiberia { get; set; }
        public bool VisitGuinea { get; set; }
        public bool VisitSierra { get; set; }
        public bool VisitMali { get; set; }
        public bool VisitKongo { get; set; }
        public bool VisitPerancis { get; set; }
        public bool VisitItalia { get; set; }
        public bool VisitJordania { get; set; }
        public bool VisitQatar { get; set; }
        public bool VisitArab { get; set; }
        public bool VisitTunisia { get; set; }
        public bool VisitInggris { get; set; }
        public bool VisitUEA { get; set; }
        public bool VisitOther { get; set; }
        public string VisitOtherText { get; set; }
        public string DurasiStart { get; set; }
        public string DurasiEnd { get; set; }
        public string TujuanPerjalanan { get; set; }
        public string TujuanPerjalananDis { get; set; }
        public string TujuanPerjalananOther { get; set; }
        public List<CheckBox> TujuanPerjalananOption { get; set; }
        public string RiwayatPaparan { get; set; }
        public string HasilDiagnosa { get; set; }
        public string HasilDiagnosaDis { get; set; }
        public string HasilDiagnosaOther { get; set; }
        public List<CheckBox> HasilDiagnosaOption { get; set; }
        public string RujukRS { get; set; }
        public List<SelectListItem> RujukRSList { get; set; }
        public string RSFlag { get; set; }
        public int IdRS { get; set; }
        public string NamaRS { get; set; }
        public string TingkatRisiko { get; set; }
        public List<CheckBox> TingkatRisikoList { get; set; }
        public string SuratRujukan { get; set; }
        public string SuratRujukanLink
        {
            get
            {
                try
                {
                    string url = string.Empty;
                    url = string.Format("{0}/{1}/{2}", PinereConstant.AttachmentUrl, this.PasienId, this.SuratRujukan);
                    if (!string.IsNullOrEmpty(this.SuratRujukan))
                    {
                        return String.Format("<a class=\"lightLink\" target=\"_blank\" href=\"{0}\">{1}</a>", url, this.SuratRujukan);
                    }
                }
                catch (Exception e)
                {
                    string errorMessage = e.Message;
                }
                return string.Empty;
            }
        }
        public string PemeriksaanFisik { get; set; }
        public string TindakanMedis { get; set; }
        public string PemeriksaanLab { get; set; }
        public string HasilLab { get; set; }
        public string Pengobatan { get; set; }
        public string KriteriaResiko { get; set; }
        public string TindakanResiko { get; set; }

        public KKP()
        {
            RujukRSList = new List<SelectListItem>();
            RujukRSList.Add(new SelectListItem { Value = "", Text = "-- Silahkan Pilih --" });
            RujukRSList.Add(new SelectListItem { Value = "1", Text = "Ya" });
            RujukRSList.Add(new SelectListItem { Value = "2", Text = "Tidak" });
            TujuanPerjalananOption = DataRepository.GetTujuanPerjalananList("TJ");
            HasilDiagnosaOption = DataRepository.HasilDiagnosaList("HD");
            TingkatRisikoList = DataRepository.TingkatRisikoList("RE");
        }
    }
}