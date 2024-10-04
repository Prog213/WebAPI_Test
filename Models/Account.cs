namespace WebApplication1.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = null!;
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<Incident>? Incidents { get; set; }
    }
}
