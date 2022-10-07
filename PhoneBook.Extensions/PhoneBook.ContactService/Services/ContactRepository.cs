using PhoneBook.Contact.Models;
using PhoneBook.ContactCommon.Models;
using PhoneBook.Extensions.MongoDB;

namespace PhoneBook.Contact.Services
{
    public interface IContactRepository : IBaseRepository<ContactModel>
    {

    }
    public class ContactRepository : BaseRepository<ContactModel>, IContactRepository
    {
        public ContactRepository(MongoDBConnectionSetting mongoDBConnectionSetting) : base(mongoDBConnectionSetting)
        {
        }
    }
}
