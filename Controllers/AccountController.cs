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
        private readonly MyDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IContactsRepository _contactsRepository;

        public AccountController(MyDbContext context, IAccountRepository accountRepository, IContactsRepository contactsRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _contactsRepository = contactsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {

            var contact = await _contactsRepository.GetByIdAsync(accountDto.ContactId);
            if (contact == null)
            {
                return NotFound($"Contact with ID {accountDto.ContactId} is not found.");
            }

            var findedAccount = await _accountRepository.GetByNameAsync(accountDto.Name);

            if (findedAccount != null)
            {
                return Conflict("An account with this name already exists.");
            }

            var account = accountDto.ToAccountModel();

            await _accountRepository.CreateAsync(account);

            var accountResponse = account.ToAccountResponseDto();

            return CreatedAtAction(nameof(CreateAccount), new { id = account.AccountId }, accountResponse);
        }
    }
}
