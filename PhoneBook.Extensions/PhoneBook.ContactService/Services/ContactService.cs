using PhoneBook.Contact.Models;
using PhoneBook.Contact.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private Extensions.MongoDB.MongoDBConnectionSetting _mongoDBConnectionSetting;
        private IContactRepository _contactRepository;
        public ContactService(Extensions.MongoDB.MongoDBConnectionSetting mongoDBConnectionSetting,
            IContactRepository contactRepository)
        {
            _mongoDBConnectionSetting = mongoDBConnectionSetting;
            _contactRepository = contactRepository;
        }

        private List<ContactModel> GetContacts() => _contactRepository.GetList();

        public List<ContactModel> AddContact(ContactModel model)
        {
            _contactRepository.InsertItem(model);
            return GetAll();
        }

        public ContactModel Get(Guid UUID)
        {
            return _contactRepository.GetByFilter(x => x.UUID == UUID);
        }

        public List<ContactModel> GetAll() => GetContacts();


        public async Task<List<ContactModel>> UpdateContact(ContactModel model)
        {
            var contact = _contactRepository.GetByFilter(x => x.UUID == model.UUID);
            contact = model;
            await _contactRepository.UpdateAsync(contact.RowId, contact);
            return GetContacts();
        }

        public List<ContactModel> DeleteContact(Guid UUID)
        {
            var contacts = GetContacts();
            var contact = contacts.FirstOrDefault(x => x.UUID == UUID);
            contacts.Remove(contact);
            return contacts;
        }

        public List<ContactModel> SpecificSearch(ContactSearchModel request)
        {
            var contacts = GetContacts();

            var result = _contactRepository.GetListByFilter(x => x.ContactInfo.InfoType == request.ContactInfoType && x.ContactInfo.InfoDetail.Contains(request.Detail)).ToList();

            return result;
        }
    }
}
