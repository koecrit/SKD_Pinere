using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinere.Models
{
    public class SearchResult
    {
        public string No { get; set; }
        public string NamaAirline { get; set; }
        public string NamaAirlineDis { get; set; }
        public string NomorPenerbangan { get; set; }
        public string WaktuBerangkat { get; set; }
        public string KotaAsal { get; set; }
        public string KotaAsalDis { get; set; }
        public string KotaTujuan { get; set; }
        public string KotaTujuanDis { get; set; }
        public string JumlahPenumpang { get; set; }
        public string PenumpangSakit { get; set; }
        public string FormGendec { get; set; }
        public string ActionEdit { get; set; }
        public string RujukRS { get; set; }
        public string NamaPasien { get; set; }
        public string TanggalLahir { get; set; }
        public string HasilDiagnosa { get; set; }
        public string RSFlag { get; set; }
        public string LitbangFlag { get; set; }
        public string HasilLab { get; set; }
        public string NamaRS { get; set; }
        public string KKPFlag { get; set; }
        public string Suspect { get; set; }
        public string Kondisi { get; set; }
        public string KKPId { get; set; }
        public string Resiko { get; set; }
    }
}