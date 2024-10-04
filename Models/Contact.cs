namespace WebApplication1.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Account>? Accounts { get; set; }
    }
}
