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

        public async Task CreateAsync(Contact contactModel)
        {
            await _context.Contacts.AddAsync(contactModel);
            await _context.SaveChangesAsync();
        }

        public Task<Contact?> GetByEmailAsync(string email)
        {
            var contact = _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
            return contact;
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact;
        }
    }
}
