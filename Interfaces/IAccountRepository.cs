using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account accountModel);
        Task<Account?> GetByNameAsync(string name);

    }
}
