using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class IncidentDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string accountName { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string contactFirstName { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string contactLastName { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string contactEmail { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Minimal lenght is 1")]
        public string incidentDescription { get; set; } = null!;
    }
}
