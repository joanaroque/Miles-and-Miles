namespace MilesBackOffice.Web.Models.Admin
{
    using CinelAirMilesLibrary.Common.Enums;
    using System.ComponentModel.DataAnnotations;

    public class InactiveUsersViewModel
    {
        public string Id { get; set; }


        [Display(Name = "Full Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }


        [EmailAddress]
        public string Email { get; set; }



        public string Username { get; set; }





        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }




        [MaxLength(20)]
        [Required]
        public string TIN { get; set; }



    }
}
