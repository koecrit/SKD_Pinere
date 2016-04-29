using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinere.Helper;

namespace Pinere.Models
{
    public class DataAirline
    {
        public int Id { get; set; }
        public string NamaAirline { get; set; }
        public string NamaAirlineDis { get; set; }
        public string NomorPenerbangan { get; set; }
        public string WaktuBerangkat { get; set; }
        public string KotaAsal { get; set; }
        public List<SelectListItem> KotaAsalList { get; set; }
        public string KotaAsalDis { get; set; }
        public string KotaTujuan { get; set; }
        public List<SelectListItem> KotaTujuanList { get; set; }
        public string KotaTujuanDis { get; set; }
        public string JumlahPenumpang { get; set; }
        public string PenumpangSakit { get; set; }
        public string PenumpangSakitDis { get; set; }
        public List<SelectListItem> PenumpangSakitList { get; set; }
        public string FormGendec { get; set; }
        public string FormGendecLink
        {
            get
            {
                try
                {
                    string url = string.Empty;
                    url = string.Format("{0}/{1}/{2}", PinereConstant.AttachmentUrl, this.Id, this.FormGendec);
                    if (!string.IsNullOrEmpty(this.FormGendec))
                    {
                        return String.Format("<a class=\"lightLink\" target=\"_blank\" href=\"{0}\">{1}</a>", url, this.FormGendec);
                    }
                }
                catch (Exception e)
                {
                    string errorMessage = e.Message;
                }
                return string.Empty;
            }
        }
        public PetugasAirlineCollection PetugasAirlineCollection { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string TipeKotaTemp { get; set; }
        public string NamaPetugas { get; set; }
        public string NomorPetugas { get; set; }
        public string KKPFlag { get; set; }
        public string JumlahSakit { get; set; }

        public DataAirline()
        {
            KotaAsalList = DataRepository.GetKodeNegara();
            KotaTujuanList = DataRepository.GetKodeNegara();
            PenumpangSakitList = new List<SelectListItem>();
            PenumpangSakitList.Add(new SelectListItem { Value = "0", Text = "-- Silahkan Pilih --" });
            PenumpangSakitList.Add(new SelectListItem { Value = "1", Text = "Ada" });
            PenumpangSakitList.Add(new SelectListItem { Value = "2", Text = "Tidak" });
            PetugasAirlineCollection = new PetugasAirlineCollection();
        }
    }

}