using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinere.Models
{
    public class PinereDataModel
    {
        public DataAirline DataAirline { get; set; }
        public Pasien Pasien { get; set; }
        public KKP KKP { get; set; }
        public Sampel Sampel { get; set; }
        public Litbang Litbang { get; set; }
        public PinereDataModel()
        {
            DataAirline = new DataAirline();
            Pasien = new Pasien();
            KKP = new KKP();
            Sampel = new Sampel();
            Litbang = new Litbang();
        }
    }
}