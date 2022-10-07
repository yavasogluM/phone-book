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
        Task<List<ContactModel>> GetAll();
        Task<ContactModel> Get(Guid UUID);
        Task<List<ContactModel>> AddContact(ContactModel model);
        Task<List<ContactModel>> UpdateContact(ContactModel model);
        Task<List<ContactModel>> DeleteContact(Guid UUID);
        Task<List<ContactModel>> SpecificSearch(ContactSearchModel request);
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

        private async Task<List<ContactModel>> GetContacts() => await _contactRepository.GetListAsync();

        public async Task<List<ContactModel>> AddContact(ContactModel model)
        {
            await _contactRepository.InsertItemAsync(model);
            return await GetContacts();
        }

        public async Task<ContactModel> Get(Guid UUID) => await _contactRepository.GetAsync(x => x.UUID == UUID);

        public async Task<List<ContactModel>> GetAll() => await GetContacts();

        public async Task<List<ContactModel>> UpdateContact(ContactModel model)
        {
            var contact = _contactRepository.GetByFilter(x => x.UUID == model.UUID);
            contact = model;
            await _contactRepository.UpdateAsync(contact.RowId, contact);
            return await GetContacts();
        }

        public async Task<List<ContactModel>> DeleteContact(Guid UUID)
        {
            await _contactRepository.DeleteItemAsync(x => x.UUID == UUID);
            return await GetContacts();
        }

        public async Task<List<ContactModel>> SpecificSearch(ContactSearchModel request) => await _contactRepository.GetListByFilterAsync(x => x.ContactInfo.InfoType == request.ContactInfoType && x.ContactInfo.InfoDetail.Contains(request.Detail));
    }
}
