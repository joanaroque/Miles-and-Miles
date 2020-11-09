using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }


        [Required]
        [DisplayName("Choose a new Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DisplayName("Repeat Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string Token { get; set; }
    }
}
