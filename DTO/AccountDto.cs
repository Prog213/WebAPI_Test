using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class AccountDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string Name { get; set; } = null!;
        [Required]
        public int ContactId { get; set; }
    }
}
