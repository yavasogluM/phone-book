using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.ReportCommon.Models
{
    public class HttpResponseReportModel
    {
        [BsonElement("uuid")]
        public string UUID { get; set; }
        [BsonElement("talep_tarihi")]
        public DateTime TalepTarihi { get; set; }
        [BsonElement("rapor_detay")]
        public ReportDetail RaporDetay { get; set; }
        [BsonElement("durum")]
        public ReportType Durum { get; set; }
    }

    public class ReportModel : Extensions.MongoDB.BaseCollection
    {
        [BsonElement("uuid")]
        public string UUID { get; set; }
        [BsonElement("talep_tarihi")]
        public DateTime TalepTarihi { get; set; }
        [BsonElement("rapor_detay")]
        public ReportDetail RaporDetay { get; set; }
        [BsonElement("durum")]
        public ReportType Durum { get; set; }
    }

    public class ReportDetail
    {
        [BsonElement("konum")]
        public string Konum { get; set; }
        [BsonElement("kisi_sayisi")]
        public int KisiSayisi { get; set; }
        [BsonElement("telefon_numarasi_sayisi")]
        public int TelefonNumarasiSayisi { get; set; }
    }

    public enum ReportType
    {
        Preparing = 0,
        Completed = 1
    }
}
