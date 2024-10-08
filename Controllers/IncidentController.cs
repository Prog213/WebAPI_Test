using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactsRepository _contactsRepository;
        private readonly IIncidentRepository _incidentRepository;

        public IncidentController(IAccountRepository accountRepository, IContactsRepository contactsRepository
            ,IIncidentRepository insidentRepository)
        {
            _accountRepository = accountRepository;
            _contactsRepository = contactsRepository;
            _incidentRepository = insidentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInsident([FromBody] IncidentDto incidentDto)
        {
            var account = await _accountRepository.GetByNameAsync(incidentDto.accountName);
            if (account == null)
            {
                return NotFound($"Account with name {incidentDto.accountName} is not found.");
            }

            var contact = await _contactsRepository.GetByEmailAsync(incidentDto.contactEmail);

            if (contact != null)
            {
                await UpdateContactAndLinkToAccount(contact, account, incidentDto);

                return Ok("Contact updated succesfully");
            }
            else
            {
                await CreateContactAndLinkToAccount(account, incidentDto);

                var incidentModel = incidentDto.ToIncidentModel(account.AccountId);

                await _incidentRepository.CreateAsync(incidentModel);
                return CreatedAtAction(nameof(CreateInsident), incidentModel.ToIncidentResponseDto());
            }
        }

        private async Task UpdateContactAndLinkToAccount(Contact contact, Account account, IncidentDto incidentDto)
        {
            await _contactsRepository.UpdateAsync(contact.ContactId, incidentDto.ToContact());

            if (account.ContactId != contact.ContactId)
            {
                await _accountRepository.UpdateContactIdAsync(contact.ContactId, account);
            }
        }

        private async Task CreateContactAndLinkToAccount(Account account, IncidentDto incidentDto)
        {
            var newContact = await _contactsRepository.CreateAsync(incidentDto.ToContact());
            await _accountRepository.UpdateContactIdAsync(newContact.ContactId, account);
        }
    }
}
