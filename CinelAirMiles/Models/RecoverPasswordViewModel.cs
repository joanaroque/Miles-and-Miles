using System.ComponentModel.DataAnnotations;

namespace CinelAirMiles.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
