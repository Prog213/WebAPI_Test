namespace WebApplication1.DTO
{
    public class IncidentResponseDto
    {
        public string IncidentName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public AccountResponseDto? Account { get; set; } 
    }
}
