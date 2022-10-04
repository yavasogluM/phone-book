using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Contact.Models;
using System;
using System.Collections.Generic;

namespace PhoneBook.Contact.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        public ContactController(ILogger<ContactController> logger,
            IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpGet(template: "all")]
        public List<ContactModel> Get() => _contactService.GetAll();

        [HttpGet(template: "get/{uuid}")]
        public ContactModel Get([FromRoute] string uuid) => _contactService.Get(new Guid(uuid));

        [HttpPost(template: "add-contact")]
        public List<ContactModel> AddContact([FromBody] ContactModel request) => _contactService.AddContact(request);

        [HttpPut(template: "update-contact")]
        public List<ContactModel> UpdateContact([FromBody] ContactModel request) => _contactService.UpdateContact(request);

        [HttpDelete(template:"delete-contact/{uuid}")]
        public List<ContactModel> DeleteContact([FromRoute] string uuid) => _contactService.DeleteContact(new Guid(uuid));

        [HttpGet(template:"specific-search")]
        public List<ContactModel> SpecificSearch([FromBody] ContactSearchModel request) => _contactService.SpecificSearch(request);

    }
}
