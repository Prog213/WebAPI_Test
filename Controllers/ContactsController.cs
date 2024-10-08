using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto contactDto)
        {
            if (await _contactsRepository.ContactExists(contactDto.Email))
            {
                return Conflict("A contact with this email already exists.");
            }

            var contact = await _contactsRepository.CreateAsync(contactDto.ToContactModel());

            return CreatedAtAction(nameof(CreateContact), new { id = contact.ContactId }, contact.ToContactDto());
        }
    }
}
