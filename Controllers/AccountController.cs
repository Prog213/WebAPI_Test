using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {

            var contact = await _context.Contacts.FindAsync(accountDto.ContactId);
            if (contact == null)
            {
                return NotFound($"Contact with ID {accountDto.ContactId} is not found.");
            }

            var findedAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == accountDto.Name);

            if (findedAccount != null)
            {
                return Conflict("An account with this name already exists.");
            }

            var account = accountDto.FromAccountDto();

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            var accountResponse = account.ToAccountResponseDto();

            return CreatedAtAction(nameof(CreateAccount), new { id = account.AccountId }, accountResponse);
        }
    }
}
