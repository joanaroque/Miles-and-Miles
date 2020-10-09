using System.ComponentModel.DataAnnotations;

namespace MilesTests.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
