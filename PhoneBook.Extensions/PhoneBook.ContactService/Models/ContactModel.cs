using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PhoneBook.Contact.Models
{
    public class ContactModel : Extensions.MongoDB.BaseCollection
    {
        [BsonElement("uuid")]
        public Guid UUID { get; set; }
        [BsonElement("ad")]
        public string Ad { get; set; }
        [BsonElement("soyad")]
        public string Soyad { get; set; }
        [BsonElement("firma")]
        public string Firma { get; set; }
        [BsonElement("iletisim_bilgisi")]
        public ContactInfoModel ContactInfo { get; set; }

    }

    public class ContactInfoModel
    {
        [BsonElement("bilgi_tipi")]
        public ContactInfoType InfoType { get; set; }
        [BsonElement("bilgi_icerigi")]
        public string InfoDetail { get; set; }
    }

    public enum ContactInfoType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
}
