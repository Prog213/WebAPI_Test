using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Mappers
{
    public static class AccountMapper
    {
        public static Account FromAccountDto(this AccountDto accountDto)
        {
            return new Account
            {
                Name = accountDto.Name,
                ContactId = accountDto.ContactId,
            };
        }
        public static AccountDto ToAccountDto(this Account accountModel)
        {
            return new AccountDto
            {
                ContactId = accountModel.ContactId,
                Name = accountModel.Name
            };
        }

        public static AccountResponseDto ToAccountResponseDto(this Account accountModel)
        {
            return new AccountResponseDto
            {
                AccountId = accountModel.AccountId,
                Name = accountModel.Name,
                Contact = accountModel.Contact?.ToContactDto(),
            };
        }
    }
}
