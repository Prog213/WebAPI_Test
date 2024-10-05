﻿using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateAsync(Contact contactModel, ContactDto contactDto)
        {
            contactModel.Email = contactDto.Email;
            contactModel.FirstName = contactDto.FirstName;
            contactModel.LastName = contactDto.LastName;
            await _context.SaveChangesAsync();
        }
    }
}
