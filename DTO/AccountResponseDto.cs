using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class AccountResponseDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = null!;
        public ContactDto? Contact { get; set; }
    }
}
