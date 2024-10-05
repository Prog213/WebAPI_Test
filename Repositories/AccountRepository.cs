﻿using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;
        public AccountRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAsync (Account accountModel)
        {
            await _context.Accounts.AddAsync(accountModel);
            await _context.SaveChangesAsync();
            return accountModel;
        }

        public async Task<Account?> GetByNameAsync(string name)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);
            return account;
        }

        public async Task UpdateContactIdAsync(int contactId, Account accountModel)
        {
            accountModel.ContactId = contactId;
            await _context.SaveChangesAsync();
        }
    }
}
