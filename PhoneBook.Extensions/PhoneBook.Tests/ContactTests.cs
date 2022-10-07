using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PhoneBook.Extensions;
using System.Threading.Tasks;
using PhoneBook.ContactCommon.Models;

namespace PhoneBook.Tests
{
    public class ContactTests
    {
        private string _contactServiceUrl = "https://localhost:5001/api/contact";
        private readonly Extensions.HttpExtension _httpExtension = new HttpExtension();

        [Fact]
        public void CreateContact_Success()
        {
            var contactResponseModel = new ContactHttpResponseModel();
            contactResponseModel.UUID = Guid.NewGuid();
            contactResponseModel.Ad = "test1";
            contactResponseModel.Soyad = "test2";
            contactResponseModel.Firma = "deneme";
            contactResponseModel.ContactInfos = new List<ContactInfoModel>
            {
                 new ContactInfoModel
                 {
                      InfoDetail = "telefon",
                       InfoType = ContactInfoType.Phone
                 },
                 new ContactInfoModel
                 {
                      InfoDetail = "email@email.com",
                       InfoType = ContactInfoType.Email
                 },
                 new ContactInfoModel
                 {
                      InfoDetail = "lokasyon...",
                       InfoType = ContactInfoType.Location
                 }
            };

            var requestUrl = $"{_contactServiceUrl}/add-contact";

            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, ContactHttpResponseModel>(new Extensions.Models.HttpRequest<ContactHttpResponseModel>
            {
                RequestObject = contactResponseModel,
                RequestType = Extensions.Models.HttpRequestType.POST,
                RequestUrl = requestUrl,
            }).ResponseObject;
            Assert.IsType<List<ContactHttpResponseModel>>(result);
        }

        [Fact]
        public void DeleteContact_Success()
        {
            string uuid = "42e4951c-243e-4573-a513-5d6fe9d96c56";
            var requestUrl = $"{_contactServiceUrl}/delete-contact/{uuid}";
            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, object>(new Extensions.Models.HttpRequest<object>
            {
                RequestUrl = requestUrl,
                RequestType = Extensions.Models.HttpRequestType.DELETE
            }).ResponseObject;
        }

        [Fact]
        public void AddContactInfo_Success()
        {
            var requestUrl = $"{_contactServiceUrl}/add-contact-info";
            ContactInfoRequestModel contactInfoRequestModel = new ContactInfoRequestModel();
            contactInfoRequestModel.UUID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            contactInfoRequestModel.ContactInfo = new ContactInfoModel
            {
                InfoDetail = "test lokasyon",
                InfoType = ContactInfoType.Location
            };

            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, ContactInfoRequestModel>(new Extensions.Models.HttpRequest<ContactInfoRequestModel>
            {
                RequestUrl = requestUrl,
                RequestType = Extensions.Models.HttpRequestType.POST,
                RequestObject = contactInfoRequestModel
            }).ResponseObject;

            Assert.IsType<List<ContactHttpResponseModel>>(result);
        }

        [Fact]
        public void DeleteContactInfo_Success()
        {
            var requestUrl = $"{_contactServiceUrl}/delete-contact-info";
            ContactInfoRequestModel contactInfoRequestModel = new ContactInfoRequestModel();
            contactInfoRequestModel.UUID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            contactInfoRequestModel.ContactInfo = new ContactInfoModel
            {
                InfoDetail = "test lokasyon",
                InfoType = ContactInfoType.Location
            };

            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, ContactInfoRequestModel>(new Extensions.Models.HttpRequest<ContactInfoRequestModel>
            {
                RequestUrl = requestUrl,
                RequestType = Extensions.Models.HttpRequestType.DELETE,
                RequestObject = contactInfoRequestModel
            }).ResponseObject;

            Assert.IsType<List<ContactHttpResponseModel>>(result);
        }

        [Fact]
        public void UpdateContact_Success()
        {
            var contactResponseModel = new ContactHttpResponseModel();
            contactResponseModel.UUID = new Guid("42e4951c-243e-4573-a513-5d6fe9d96c56");
            contactResponseModel.Ad = "test1 updated";
            contactResponseModel.Soyad = "test2 updated";
            contactResponseModel.Firma = "deneme updated";
            contactResponseModel.ContactInfos = new List<ContactInfoModel>
            {
                 new ContactInfoModel
                 {
                      InfoDetail = "telefon",
                       InfoType = ContactInfoType.Phone
                 },
                 new ContactInfoModel
                 {
                      InfoDetail = "email@email.com",
                       InfoType = ContactInfoType.Email
                 },
                 new ContactInfoModel
                 {
                      InfoDetail = "lokasyon...",
                       InfoType = ContactInfoType.Location
                 }
            };

            var requestUrl = $"{_contactServiceUrl}/update-contact";
            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, ContactHttpResponseModel>(new Extensions.Models.HttpRequest<ContactHttpResponseModel>
            {
                RequestObject = contactResponseModel,
                RequestType = Extensions.Models.HttpRequestType.PUT,
                RequestUrl = requestUrl,
            }).ResponseObject;
            Assert.IsType<List<ContactHttpResponseModel>>(result);
        }

        [Fact]
        public void GetContacts_Success()
        {
            var result = _httpExtension.GetResponse<List<ContactHttpResponseModel>, object>(new Extensions.Models.HttpRequest<object>
            {
                RequestUrl = $"{_contactServiceUrl}/all",
                RequestType = Extensions.Models.HttpRequestType.GET
            }).ResponseObject;

            Assert.IsType<List<ContactHttpResponseModel>>(result);
        }
    }
}
