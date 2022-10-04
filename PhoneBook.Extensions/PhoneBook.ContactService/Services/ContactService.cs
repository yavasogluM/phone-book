using PhoneBook.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Contact
{
    public interface IContactService
    {
        List<ContactModel> GetAll();
        ContactModel Get(Guid UUID);
        List<ContactModel> AddContact(ContactModel model);
        List<ContactModel> UpdateContact(ContactModel model);
        List<ContactModel> DeleteContact(Guid UUID);
        List<ContactModel> SpecificSearch(ContactSearchModel request);
    }
    public class ContactService : IContactService
    {
        private List<ContactModel> GetDummyContacts()
        {
            return new List<ContactModel>
                {
                    new ContactModel{ UUID = new Guid("47abddd9-78f8-41f7-99ce-f9211f45473a"), Ad = "ad1", Soyad = "soyad1", ContactInfo = new ContactInfoModel{
                     InfoDetail = "test@test.com", InfoType = ContactInfoType.Email
                    } },
                    new ContactModel{ UUID = new Guid("5d170477-aa91-46c6-a139-4a5a47ac632b"), Ad = "ad2", Soyad = "soyad2", ContactInfo = new ContactInfoModel{
                     InfoDetail = "ISTANBUL", InfoType = ContactInfoType.Location
                    } },
                };
        }

        public List<ContactModel> AddContact(ContactModel model)
        {
            var _contacts = GetDummyContacts();
            _contacts.Add(model);
            return _contacts;
        }

        public ContactModel Get(Guid UUID)
        {
            return GetDummyContacts().FirstOrDefault(x => x.UUID == UUID);
        }

        public List<ContactModel> GetAll()
        {
            return GetDummyContacts();
        }

        public List<ContactModel> UpdateContact(ContactModel model)
        {
            var contacts = GetDummyContacts();
            var contact = contacts.FirstOrDefault(x => x.UUID == model.UUID);
            contact = model;
            return contacts;
        }

        public List<ContactModel> DeleteContact(Guid UUID)
        {
            var contacts = GetDummyContacts();
            var contact = contacts.FirstOrDefault(x => x.UUID == UUID);
            contacts.Remove(contact);
            return contacts;
        }

        public List<ContactModel> SpecificSearch(ContactSearchModel request)
        {
            var contacts = GetDummyContacts();

            var result = contacts.Where(x => x.ContactInfo.InfoType == request.ContactInfoType && x.ContactInfo.InfoDetail.Contains(request.Detail)).ToList();

            return result;
        }
    }
}
