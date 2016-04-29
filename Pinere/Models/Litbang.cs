using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinere.Helper;

namespace Pinere.Models
{
    public class Litbang
    {
        public int Id { get; set; }
        public string PasienId { get; set; }
        public string TanggalPeriksaLab { get; set; }
        public string HasilLab { get; set; }
        public List<CheckBox> HasilLabOption { get; set; }
        public string Keterangan { get; set; }
        public string Attachment { get; set; }
        public string AttachmentLink
        {
            get
            {
                try
                {
                    string url = string.Empty;
                    url = string.Format("{0}/{1}/{2}", PinereConstant.AttachmentUrl, this.PasienId, this.Attachment);
                    if (!string.IsNullOrEmpty(this.Attachment))
                    {
                        return String.Format("<a class=\"lightLink\" target=\"_blank\" href=\"{0}\">{1}</a>", url, this.Attachment);
                    }
                }
                catch (Exception e)
                {
                    string errorMessage = e.Message;
                }
                return string.Empty;
            }
        }
        public string TanggalTerimaSampel { get; set; }

        public Litbang()
        {
            HasilLabOption = new List<CheckBox>();
            HasilLabOption.Add(new CheckBox { Id = "RBPositif", Text = "Positif", Value = "Positif" });
            HasilLabOption.Add(new CheckBox { Id = "RBNegatif", Text = "Negatif", Value = "Negatif" });
        }
    }
}