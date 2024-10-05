using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.DTO
{
    public class ContactDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string LastName { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = null!;
    }
}
