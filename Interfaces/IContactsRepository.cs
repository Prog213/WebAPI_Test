using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IContactsRepository
    {
        Task CreateAsync(Contact contactModel);
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact?> GetByEmailAsync(string email);

    }
}
