using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinere.Models
{
    public class PetugasAirlineCollection
    {
        public string No { get; set; }
        public string Nama { get; set; }
        public string NomorPegawai { get; set; }
        public string WargaNegara { get; set; }
        public List<SelectListItem> WargaNegaraList { get; set; }
        public string Passport { get; set; }
        public string Kelamin { get; set; }
        public List<SelectListItem> KelaminList { get; set; }
        public PetugasAirline[] PetugasAirline { get; set; }

        public PetugasAirlineCollection()
        {
            PetugasAirline = new List<PetugasAirline>().ToArray();
            WargaNegaraList = DataRepository.GetKodeNegara();

            KelaminList = new List<SelectListItem>();
            KelaminList.Add(new SelectListItem { Value = "0", Text = "-- Silahkan Pilih --" });
            KelaminList.Add(new SelectListItem { Value = "1", Text = "Laki-Laki" });
            KelaminList.Add(new SelectListItem { Value = "2", Text = "Perempuan" });
        }

    }
}