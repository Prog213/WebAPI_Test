using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IContactsRepository _contactsRepository;
        private readonly IIncidentRepository _insidentRepository;

        public IncidentController(MyDbContext context, IAccountRepository accountRepository, IContactsRepository contactsRepository
            ,IIncidentRepository insidentRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _contactsRepository = contactsRepository;
            _insidentRepository = insidentRepository;
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
            var contactDto = incidentDto.FromIncidentDtoToContactDto();

            if (contact != null)
            {
                await _contactsRepository.UpdateAsync(contact, contactDto);
                
                if (account.ContactId != contact.ContactId)
                {
                    await _accountRepository.UpdateContactIdAsync(contact.ContactId, account);
                }

                return Ok("Contact updated succesfully");
            }
            else
            {
                var newContact = await _contactsRepository.CreateAsync(contactDto.ToContactModel());
                await _accountRepository.UpdateContactIdAsync(newContact.ContactId, account);

                var incidentModel = incidentDto.ToIncidentModel(account.AccountId);
                await _insidentRepository.CreateAsync(incidentModel);
                return CreatedAtAction(nameof(CreateInsident), incidentModel.ToIncidentResponseDto());
            }

        }
    }
}
