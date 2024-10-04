namespace WebApplication1.Models
{
    public class Incident
    {
        public string IncidentName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
