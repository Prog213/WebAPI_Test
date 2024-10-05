using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ContactsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto contactDto)
        {

            var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == contactDto.Email);
            if (existingContact != null)
            {
                return Conflict("A contact with this email already exists.");
            }

            var contact = contactDto.FromContactDto();

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateContact), new { id = contact.ContactId }, contact.ToContactDto());
        }
    }
}
