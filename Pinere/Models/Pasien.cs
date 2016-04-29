using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinere.Helper;

namespace Pinere.Models
{
    public class Pasien
    {
        public int Id { get; set; }
        public string DataAirlineId { get; set; }
        public string Nama { get; set; }
        public string TanggalLahir { get; set; }
        public string Kelamin { get; set; }
        public List<SelectListItem> KelaminList { get; set; }
        public string Pekerjaan { get; set; }
        public string Passport { get; set; }
        public string KTP { get; set; }
        public string Alamat { get; set; }
        public string NamaAirline { get; set; }
        public string NomorPenerbangan { get; set; }
        public string NomorTelpon { get; set; }
        public Pasien()
        {
            KelaminList = new List<SelectListItem>();
            KelaminList.Add(new SelectListItem { Value = "", Text = "-- Silahkan Pilih --" });
            KelaminList.Add(new SelectListItem { Value = "L", Text = "Laki-Laki" });
            KelaminList.Add(new SelectListItem { Value = "P", Text = "Perempuan" });
        }
    }
}