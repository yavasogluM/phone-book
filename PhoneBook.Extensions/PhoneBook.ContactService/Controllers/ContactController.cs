using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Contact.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<ContactModel>> Get() => await _contactService.GetAll();

        [HttpGet(template: "get/{uuid}")]
        public async Task<ContactModel> Get([FromRoute] string uuid) => await _contactService.Get(new Guid(uuid));

        [HttpPost(template: "add-contact")]
        public async Task<List<ContactModel>> AddContact([FromBody] ContactModel request) => await _contactService.AddContact(request);

        [HttpPut(template: "update-contact")]
        public async Task<List<ContactModel>> UpdateContact([FromBody] ContactModel request) => await _contactService.UpdateContact(request);

        [HttpDelete(template:"delete-contact/{uuid}")]
        public async Task<List<ContactModel>> DeleteContact([FromRoute] string uuid) => await _contactService.DeleteContact(new Guid(uuid));

        [HttpGet(template:"specific-search")]
        public async Task<List<ContactModel>> SpecificSearch([FromBody] ContactSearchModel request) => await _contactService.SpecificSearch(request);

    }
}
