using System;

namespace PhoneBook.Contact.Models
{
    public class ContactModel
    {
        public Guid UUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public ContactInfoModel ContactInfo { get; set; }

    }

    public class ContactInfoModel
    {
        public ContactInfoType InfoType { get; set; }
        public string InfoDetail { get; set; }
    }

    public enum ContactInfoType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
}
