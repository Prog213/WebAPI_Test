using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactsRepository _contactsRepository;

        public AccountController(IAccountRepository accountRepository, IContactsRepository contactsRepository)
        {
            _accountRepository = accountRepository;
            _contactsRepository = contactsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        { 
            if (!await _contactsRepository.ContactExists(accountDto.ContactId))
            {
                return NotFound($"Contact with ID {accountDto.ContactId} is not found.");
            }

            if (await _accountRepository.AccountExists(accountDto.Name))
            {
                return Conflict("An account with this name already exists.");
            }

            var account = await _accountRepository.CreateAsync(accountDto.ToAccountModel());

            return CreatedAtAction(nameof(CreateAccount), new { id = account.AccountId }, account.ToAccountResponseDto());
        }
    }
}
