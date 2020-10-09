using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilesTests.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }



        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        [RegularExpression(@"\d{9}",
         ErrorMessage = "Must insert the {0} correct.")]
        public string PhoneNumber { get; set; }


        public IEnumerable<SelectListItem> Roles { get; set; }


        [Display(Name = "Role")]
        public string SelectedRole { get; set; }



    }
}
