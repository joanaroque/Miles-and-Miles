using System.ComponentModel.DataAnnotations;

namespace CinelAirMiles.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        public string GuidId { get; set; }
    }
}
