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
        private readonly MyDbContext _context;
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(MyDbContext context, IContactsRepository contactsRepository)
        {
            _context = context;
            _contactsRepository = contactsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto contactDto)
        {

            var existingContact = await _contactsRepository.GetByEmailAsync(contactDto.Email);
            if (existingContact != null)
            {
                return Conflict("A contact with this email already exists.");
            }

            var contact = contactDto.ToContactModel();

            await _contactsRepository.CreateAsync(contact);

            return CreatedAtAction(nameof(CreateContact), new { id = contact.ContactId }, contact.ToContactDto());
        }
    }
}
