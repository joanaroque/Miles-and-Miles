namespace CinelAirMiles.Models
{
    using Microsoft.AspNetCore.Authentication;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClientLoginViewModel
    {
        [Required]
        public string UserName { get; set; }


        public string Email { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }


        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
