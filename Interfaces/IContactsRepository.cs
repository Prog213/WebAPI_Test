using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IContactsRepository
    {
        Task<Contact> CreateAsync(Contact contactModel);
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact?> GetByEmailAsync(string email);
        Task UpdateAsync (int id, Contact contact);
        Task<bool> ContactExists(int id);
        Task<bool> ContactExists(string email);
    }
}
