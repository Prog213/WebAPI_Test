using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly MyDbContext _context;
        public ContactsRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ContactExists(int id)
        {
            return await _context.Contacts.AnyAsync(c => c.ContactId == id);
        }

        public async Task<bool> ContactExists(string email)
        {
            return await _context.Contacts.AnyAsync(c => c.Email == email);
        }

        public async Task<Contact> CreateAsync(Contact contactModel)
        {
            await _context.Contacts.AddAsync(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<Contact?> GetByEmailAsync(string email)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
            return contact;
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact;
        }

        public async Task UpdateAsync(int id, Contact contact)
        {
            var existingContact = await GetByIdAsync(id);

            if (existingContact != null)
            {
                existingContact.Email = contact.Email;
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                await _context.SaveChangesAsync();
            }
        }
    }
}
