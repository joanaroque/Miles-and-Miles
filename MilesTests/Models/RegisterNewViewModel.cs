using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class RegisterNewViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
       // [Required(ErrorMessage = "The field {0} is mandatory.")]
        //[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Display(Name = "Username")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Username { get; set; }


        [Display(Name = "New Password")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must contain between {2} and {1} characters.")]
        public string Password { get; set; }


        [Display(Name = "Password Confirm")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must contain between {2} and {1} characters.")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }


        

    }
}
