using Pinere.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Http;
using System.Globalization;


namespace Pinere.Models
{
    public class DataRepository
    {
        public static DataAirline GetDataAirline(int Id)
        {
            DataAirline DataAirline = new DataAirline();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    DataAirline = (from a in dc.GetDataAirline(Id)
                                   select new DataAirline
                              {
                                  Id = a.ID,
                                  NamaAirline = a.Nama_Airline.Value.ToString(),
                                  NamaAirlineDis = a.Nama_Airline_Dis,
                                  NomorPenerbangan = a.Nomor_Penerbangan,
                                  WaktuBerangkat = GetDateStringFromDate(a.Waktu_Berangkat),
                                  KotaAsal = a.Kota_Asal,
                                  KotaAsalDis = a.Kota_Asal_Dis,
                                  KotaTujuan = a.Kota_Tujuan,
                                  KotaTujuanDis = a.Kota_Tujuan_Dis,
                                  JumlahPenumpang = a.Jumlah_Penumpang.Value.ToString(),
                                  PenumpangSakit = a.Penumpang_Sakit.Value.ToString(),
                                  PenumpangSakitDis = a.Penumpang_Sakit_Dis,
                                  FormGendec = a.Form_Gendec,
                                  CreatedBy = a.CreatedBy,
                                  CreatedDate = GetDateStringFromDate(a.CreatedDate),
                                  UpdatedBy = a.UpdatedBy,
                                  UpdatedDate = GetDateStringFromDate(a.UpdatedDate),
                                  NamaPetugas = a.Nama_Petugas,
                                  NomorPetugas = a.Nomor_Petugas,
                                  JumlahSakit = a.Jumlah_Sakit.Value.ToString()
                              }).SingleOrDefault();
                    if (DataAirline == null)
                    {
                        DataAirline = new DataAirline();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return DataAirline;
        }
        public static Pasien GetPasien(int Id)
        {
            Pasien Pasien = new Pasien();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    Pasien = (from a in dc.GetPasien(Id)
                              select new Pasien
                                   {
                                       Id = a.Id,
                                       DataAirlineId = a.DataAirlineId.Value.ToString(),
                                       Nama = a.Nama,
                                       TanggalLahir = GetDateStringFromDate(a.TanggalLahir),
                                       Kelamin = a.Kelamin,
                                       Pekerjaan = a.Pekerjaan,
                                       Passport = a.Passport,
                                       KTP = a.KTP,
                                       Alamat = a.Alamat,
                                       NamaAirline = a.NamaAirline,
                                       NomorPenerbangan = a.NomorPenerbangan,
                                       NomorTelpon = a.NomorTelpon
                                   }).SingleOrDefault();
                    if (Pasien == null)
                    {
                        Pasien = new Pasien();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Pasien;
        }
        public static KKP GetDataKKP(int Id)
        {
            KKP KKP = new KKP();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    KKP = (from a in dc.GetDataKKP(Id)
                           select new KKP
                              {
                                  Id = a.Id,
                                  PasienId = a.PasienId.Value.ToString(),
                                  Suhu = a.Suhu.Value.ToString(),
                                  TekananDarah1 = a.Tekanan_Darah1.Value.ToString(),
                                  TekananDarah2 = a.Tekanan_Darah2.Value.ToString(),
                                  Pernapasan = a.Pernapasan.Value.ToString(),
                                  Nadi = a.Nadi.Value.ToString(),
                                  TandaGejala = a.Tanda_Gejala,
                                  VisitLiberia = a.Visit_Liberia.Value,
                                  VisitGuinea = a.Visit_Guinea.Value,
                                  VisitSierra = a.Visit_Sierra.Value,
                                  VisitMali = a.Visit_Mali.Value,
                                  VisitKongo = a.Visit_Kongo.Value,
                                  VisitPerancis = a.Visit_Perancis.Value,
                                  VisitItalia = a.Visit_Italia.Value,
                                  VisitJordania = a.Visit_Jordania.Value,
                                  VisitQatar = a.Visit_Qatar.Value,
                                  VisitArab = a.Visit_Arab.Value,
                                  VisitTunisia = a.Visit_Tunisia.Value,
                                  VisitInggris = a.Visit_Inggris.Value,
                                  VisitUEA = a.Visit_UEA.Value,
                                  VisitOther = a.Visit_Other.Value,
                                  VisitOtherText = a.Visit_Other_Text,
                                  DurasiStart = GetDateStringFromDate(a.Durasi_Start),
                                  DurasiEnd = GetDateStringFromDate(a.Durasi_End),
                                  TujuanPerjalanan = a.Tujuan_Perjalanan,
                                  TujuanPerjalananOther = a.Tujuan_Perjalanan_Other,
                                  RiwayatPaparan = a.Riwayat_Paparan,
                                  HasilDiagnosa = a.Hasil_Diagnosa,
                                  HasilDiagnosaOther = a.Hasil_Diagnosa_Other,
                                  RujukRS = a.Rujuk_RS,
                                  IdRS = a.IdRS,
                                  NamaRS = a.NamaRS,
                                  SuratRujukan = a.Surat_Rujukan,
                                  TingkatRisiko = a.Tingkat_Risiko,
                                  PemeriksaanFisik = a.Pemeriksaan_fisik,
                                  TindakanMedis = a.Tindakan_medis,
                                  PemeriksaanLab = a.Pemeriksaan_lab,
                                  HasilLab = a.Hasil_pemeriksaan_lab,
                                  Pengobatan = a.Pengobatan,
                                  KriteriaResiko = a.Kriteria_Resiko,
                                  TindakanResiko = a.Tindakan_Resiko
                              }).SingleOrDefault();
                    if (KKP == null)
                    {
                        KKP = new KKP();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return KKP;
        }
        public static Sampel GetSampel(int Id)
        {
            Sampel Sampel = new Sampel();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    Sampel = (from a in dc.GetDataSampel(Id)
                              select new Sampel
                              {
                                  Id = a.Id,
                                  PasienId = a.PasienId.Value.ToString(),
                                  TanggalAmbilSampel = GetDateStringFromDate(a.Tanggal_ambil_sampel),
                                  JenisSpesimen = a.Jenis_spesimen,
                                  JenisPemeriksaanLab = a.Jenis_pemeriksaan_lab,
                                  TanggalKirimSampel = GetDateStringFromDate(a.Tanggal_kirim_sampel),
                                  TanggalKeluarRS = GetDateStringFromDate(a.Tanggal_keluar_rs),
                                  Kondisi = a.Kondisi
                              }).SingleOrDefault();
                    if (Sampel == null)
                    {
                        Sampel = new Sampel();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Sampel;
        }
        public static Litbang GetLitbang(int Id)
        {
            Litbang Litbang = new Litbang();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    Litbang = (from a in dc.GetDataLitbang(Id)
                              select new Litbang
                              {
                                  Id = a.Id,
                                  PasienId = a.PasienId.Value.ToString(),
                                  TanggalPeriksaLab = GetDateStringFromDate(a.Tanggal_periksa_lab),
                                  HasilLab = a.Hasil_lab,
                                  Keterangan = a.Keterangan,
                                  Attachment = a.Attachment,
                                  TanggalTerimaSampel = GetDateStringFromDate(a.Tanggal_terima_sampel)
                              }).SingleOrDefault();
                    if (Litbang == null)
                    {
                        Litbang = new Litbang();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Litbang;
        }
        public static List<PetugasAirline> GetPetugasAirline(int Id)
        {
            List<PetugasAirline> PetugasAirline = new List<PetugasAirline>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    PetugasAirline = (from a in dc.GetPetugasAirline(Id)
                                   select new PetugasAirline
                                   {
                                       No = a.NO.Value,
                                       Id = a.ID,
                                       IdDataAirline = a.Id_Data_Airline.Value.ToString(),
                                       Nama = a.Nama,
                                       NomorPegawai = a.Nomor_Pegawai,
                                       WargaNegara = a.Warga_Negara,
                                       Passport = a.Passport,
                                       Kelamin = a.Kelamin,
                                   }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return PetugasAirline;
        }
        public static Boolean SaveDataAirline(PinereDataContext dc, string User, DataAirline ai, out string DataAirlineId)
        {
            DataAirlineId = string.Empty;
            try
            {
                var DataAirline = (from a in dc.InsertDataAirline(ai.Id, int.Parse(ai.NamaAirline), ai.NomorPenerbangan, GetDateFromStringDate(ai.WaktuBerangkat), ai.KotaAsal, ai.KotaTujuan,
                    int.Parse(ai.JumlahPenumpang), int.Parse(ai.PenumpangSakit), ai.FormGendec, DateTime.Now, User, ai.NamaPetugas, ai.NomorPetugas, int.Parse(ai.JumlahSakit))
                                   select a).SingleOrDefault();
                DataAirlineId = DataAirline.DataAirlineId.ToString();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean SavePetugasAirline(PinereDataContext dc, int DataAirlineId, string User, PetugasAirline[] pa)
        {
            try
            {
                dc.DeletePetugasAirline(DataAirlineId);
                for (int i = 0; i < pa.Count(); i++)
                {
                    dc.InsertPetugasAirline(pa[i].Id, DataAirlineId, pa[i].Nama, pa[i].NomorPegawai, pa[i].WargaNegara, pa[i].Passport,
                        pa[i].Kelamin, DateTime.Now, User);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean SavePasien(PinereDataContext dc, string User,int DataAirlineId, Pasien ps, out string PasienId)
        {
            PasienId = string.Empty;
            try
            {
                var Pasien = (from a in dc.InsertPasien(ps.Id, DataAirlineId, ps.Nama, GetDateFromStringDate(ps.TanggalLahir), ps.Kelamin, ps.Pekerjaan, ps.Passport,
                    ps.KTP, ps.Alamat, ps.NamaAirline, ps.NomorPenerbangan, ps.NomorTelpon, DateTime.Now, User)
                              select a).SingleOrDefault();
                PasienId = Pasien.PasienId.ToString();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean SaveDataKKP(PinereDataContext dc, string User, KKP kp, int PasienId)
        {
            try
            {
                dc.InsertKKP(kp.Id, PasienId, decimal.Parse(kp.Suhu), decimal.Parse(kp.TekananDarah1), decimal.Parse(kp.TekananDarah2), int.Parse(kp.Pernapasan), int.Parse(kp.Nadi),
                    kp.TandaGejala, kp.VisitLiberia, kp.VisitGuinea, kp.VisitSierra, kp.VisitMali, kp.VisitKongo, kp.VisitPerancis, kp.VisitItalia, kp.VisitJordania, kp.VisitQatar,
                    kp.VisitArab, kp.VisitTunisia, kp.VisitInggris, kp.VisitUEA, kp.VisitOther, kp.VisitOtherText, GetDateFromStringDate(kp.DurasiStart), GetDateFromStringDate(kp.DurasiEnd), kp.TujuanPerjalanan,
                    kp.TujuanPerjalananOther, kp.RiwayatPaparan, kp.HasilDiagnosa, kp.HasilDiagnosaOther, kp.RujukRS, kp.IdRS, kp.SuratRujukan, kp.TingkatRisiko,
                     kp.PemeriksaanFisik, kp.TindakanMedis, kp.PemeriksaanLab, kp.HasilLab, kp.Pengobatan, kp.KriteriaResiko, kp.TindakanResiko);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean SaveDataSampel(PinereDataContext dc, string User, Sampel sm, int PasienId)
        {
            try
            {
                dc.InsertDataSampel(sm.Id, PasienId, GetDateFromStringDate(sm.TanggalAmbilSampel), sm.JenisSpesimen, sm.JenisPemeriksaanLab, GetDateFromStringDate(sm.TanggalKirimSampel), DateTime.Now, User, GetDateFromStringDate(sm.TanggalKeluarRS),sm.Kondisi);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean SaveDataLitbang(PinereDataContext dc, string User, Litbang lb, int PasienId)
        {
            try
            {
                dc.InsertDataLitbang(lb.Id, PasienId, GetDateFromStringDate(lb.TanggalPeriksaLab), lb.HasilLab, lb.Keterangan, lb.Attachment, DateTime.Now, User, GetDateFromStringDate(lb.TanggalTerimaSampel));
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean UpdateKKPFlag(PinereDataContext dc, int DataAirlineId)
        {
            try
            {
                dc.UpdateDataAirlineForKKP(DataAirlineId);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean UpdateRSFlag(PinereDataContext dc, int KKPId)
        {
            try
            {
                dc.UpdateRSFlag(KKPId);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static Boolean UpdateLitbangFlag(PinereDataContext dc, int SampelId)
        {
            try
            {
                dc.UpdateLitbangFlag(SampelId);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int GetTotalDataForKKP()
        {
            int total = 0;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var totalQ = dc.GetTotalDataAirlineForKKP().SingleOrDefault();
                    if (totalQ != null)
                    {
                        total = totalQ.Total.Value;
                    }
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int GetTotalDataForRS(int rsId)
        {
            int total = 0;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var totalQ = dc.GetTotalDataPasienForRS(rsId).SingleOrDefault();
                    if (totalQ != null)
                    {
                        total = totalQ.Total.Value;
                    }
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int GetTotalDataForLitbang()
        {
            int total = 0;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var totalQ = dc.GetTotalDataPasienForLitbang().SingleOrDefault();
                    if (totalQ != null)
                    {
                        total = totalQ.Total.Value;
                    }
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string GetKriteriaBasedOnResiko(string Category, string Resiko)
        {
            string kriteria = string.Empty;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var res = dc.GetKriteriaBasedOnResiko(Category,Resiko).SingleOrDefault();
                    if (res != null)
                    {
                        kriteria = res.kriteria;
                    }
                }
                return kriteria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string GetTindakanBasedOnResiko(string Category, string Resiko)
        {
            string tindakan = string.Empty;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var res = dc.GetTindakanBasedOnResiko(Category, Resiko).SingleOrDefault();
                    if (res != null)
                    {
                        tindakan = res.tindakan;
                    }
                }
                return tindakan;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<CheckBox> GetTujuanPerjalananList(string type)
        {
            List<CheckBox> model = new List<CheckBox>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    model = (from a in dc.tbl_tujuans
                             select new CheckBox()
                             {
                                 Id = type + "_" + a.Kode,
                                 Value = a.Kode,
                                 Text = a.Tujuan
                             }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return model;
        }
        public static List<CheckBox> HasilDiagnosaList(string type)
        {
            List<CheckBox> model = new List<CheckBox>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    model = (from a in dc.tbl_hasil_diagnosas
                             select new CheckBox()
                             {
                                 Id = type + "_" + a.Kode,
                                 Value = a.Kode,
                                 Text = a.Hasil
                             }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return model;
        }
        public static List<CheckBox> TingkatRisikoList(string type)
        {
            List<CheckBox> model = new List<CheckBox>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    model = (from a in dc.tbl_tingkat_resikos
                             select new CheckBox()
                             {
                                 Id = type + "_" + a.KODE,
                                 Value = a.KODE,
                                 Text = a.RESIKO
                             }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return model;
        }

        public static List<City> GetKota()
        {
            List<City> CityList = new List<City>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    CityList = (from a in dc.GetKota()
                                    select new City
                                    {
                                        Code = a.Kode_Kota,
                                        Name = a.Nama_Kota,
                                        Country = a.Negara
                                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return CityList;
        }
        public static List<Maskapai> GetMaskapai()
        {
            List<Maskapai> MaskapaiList = new List<Maskapai>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    MaskapaiList = (from a in dc.GetMaskapai()
                                select new Maskapai
                                {
                                    Id = a.ID.ToString(),
                                    Name = a.Maskapai,
                                    Country = a.Negara
                                }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return MaskapaiList;
        }
        public static List<RS> GetRS()
        {
            List<RS> RSList = new List<RS>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    RSList = (from a in dc.GetRS()
                                    select new RS
                                    {
                                        Id = a.Id.ToString(),
                                        Nama = a.Nama,
                                        Alamat = a.Alamat
                                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return RSList;
        }
        public static List<SelectListItem> GetKodeNegara()
        {
            string errorMessage = string.Empty;
            List<SelectListItem> KodeNegaraList = new List<SelectListItem>();
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    KodeNegaraList = (from a in dc.tbl_kode_negaras
                                      select new SelectListItem { Value = a.Kode_Negara, Text = a.Negara }).Distinct().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            KodeNegaraList.Insert(0, new SelectListItem { Value = "", Text = "-- Silahkan Pilih --", Selected = true });
            return KodeNegaraList;
        }
        public static Boolean IsAuthorizedUser(string UserId, string Password)
        {
            string errorMessage = string.Empty;
            Boolean isAuthorized = false;
            try
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    int isAuthorizedTemp = (from a in dc.IsAuthorizedUser(UserId, Password)
                                            select a.IsAuthorized.Value).SingleOrDefault();
                    if (isAuthorizedTemp == 1)
                    {
                        isAuthorized = true;
                    }
                    return isAuthorized;
                }
            }
            catch (Exception e)
            {
                return isAuthorized;
            }
        }
        public static String GetDateTimeStringFromDate(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("dd-MMM-yyyy HH:mm");
            }
            else
            {
                return string.Empty;
            }
        }
        public static DateTime? GetDateFromStringDateTime(String value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return DateTime.Parse(value);
            }
            else
            {
                return null;
            }
        }
        public static String GetDateStringFromDate(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                return "";
            }
        }
        public static DateTime? GetDateFromStringDate(String value)
        {
            if (!string.IsNullOrEmpty(value) && value != "-")
            {
                return DateTime.Parse(value);
            }
            else
            {
                return null;
            }
        }
        public static String GetDateStringFromDate2(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("dd-MMM-yy");
            }
            else
            {
                return "";
            }
        }

    }
}