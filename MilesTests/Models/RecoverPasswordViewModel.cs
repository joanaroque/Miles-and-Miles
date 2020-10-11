using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
