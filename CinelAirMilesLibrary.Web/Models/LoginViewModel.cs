namespace CinelAirMilesLibrary.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Authentication;

    public class LoginViewModel
    {
        public string UserName { get; set; }


        public string Email { get; set; }

        [RegularExpression(@"\d{9}",
         ErrorMessage = "Must insert the {0} correct.")]
        public string GuidId { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }


        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
