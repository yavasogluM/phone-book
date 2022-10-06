using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.ReportCommon.Models
{
    public class ReportModel
    {
        public int UUID { get; set; }
        public DateTime TalepTarihi { get; set; }
        public ReportDetail RaporDetay { get; set; }
        public ReportType Durum { get; set; }
    }

    public class ReportDetail
    {
        public string Konum { get; set; }
        public int KisiSayisi { get; set; }
        public int TelefonNumarasiSayisi { get; set; }
    }

    public enum ReportType
    {
        Preparing = 0,
        Completed = 1
    }
}
