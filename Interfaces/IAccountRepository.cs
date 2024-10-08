using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account accountModel);
        Task<Account?> GetByNameAsync(string name);
        Task UpdateContactIdAsync(int contactId, Account accountModel);
        Task<bool> AccountExists(string name);

    }
}
