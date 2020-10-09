using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace MilesTests.Data.Entities
{
    public class User : IdentityUser
    {

        [Required(ErrorMessage = "Must insert the {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }




        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }



        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";


        [RegularExpression(@"\d{9}",
         ErrorMessage = "Must insert the {0} correct.")]
        [Display(Name = "Telephone")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

    }
}
