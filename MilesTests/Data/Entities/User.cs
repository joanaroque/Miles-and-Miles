using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace MilesTests.Data.Entities
{
    public class User : IdentityUser
    {

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
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
