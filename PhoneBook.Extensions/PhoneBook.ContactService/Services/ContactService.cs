using PhoneBook.Contact.Models;
using PhoneBook.Contact.Services;
using PhoneBook.ContactCommon.Models;
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
        Task<List<ContactModel>> AddContactInfo(Guid uuid, ContactInfoModel contactInfo);
        Task<List<ContactModel>> DeleteContactInfo(Guid uuid, ContactInfoModel contactInfo);
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
            var _id = contact.Id;
            contact = model;
            contact.Id = _id;
            await _contactRepository.UpdateAsync(contact.RowId, contact);
            return await GetContacts();
        }

        public async Task<List<ContactModel>> DeleteContact(Guid UUID)
        {
            await _contactRepository.DeleteItemAsync(x => x.UUID == UUID);
            return await GetContacts();
        }

        public async Task<List<ContactModel>> SpecificSearch(ContactSearchModel request)
        {
            var list = await _contactRepository.GetListByFilterAsync(x => x.ContactInfos.Any(y => y.InfoType == request.ContactInfoType && y.InfoDetail.Contains(request.Detail)));
            return list;
        }

        public async Task<List<ContactModel>> AddContactInfo(Guid uuid, ContactInfoModel contactInfo)
        {
            var contact = _contactRepository.GetByFilter(x => x.UUID == uuid);
            if (contact == null)
                throw new Exception($"There is no contact with Id:{uuid}"); //Example exception 

            if (contact.ContactInfos == null)
                contact.ContactInfos = new List<ContactInfoModel>();
            contact.ContactInfos.Add(contactInfo);
            await _contactRepository.UpdateAsync(contact.RowId, contact);
            return await GetContacts();
        }

        public async Task<List<ContactModel>> DeleteContactInfo(Guid uuid, ContactInfoModel contactInfo)
        {
            var contact = await _contactRepository.GetAsync(x => x.UUID == uuid);
            if (contact == null)
                throw new Exception($"There is no contact with Id:{uuid}"); //Example exception 

            var selectedContactInfo = contact.ContactInfos.FirstOrDefault(x => x.InfoType == contactInfo.InfoType && x.InfoDetail == contactInfo.InfoDetail);
            if (selectedContactInfo != null)
                contact.ContactInfos.Remove(selectedContactInfo);

            await _contactRepository.UpdateAsync(contact.RowId, contact);
            return await GetContacts();
        }
    }
}
