using MilesBackOffice.Web.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class UserRoleViewModel 
    {
        public string UserId { get; set; }


        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        public IEnumerable<string> Roles { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime DateOfBirth { get; set; }


        [MaxLength(20)]
        [Required]
        public string Document { get; set; }


        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        [RegularExpression(@"\d{9}",
        ErrorMessage = "Must insert the {0} correct.")]
        public string PhoneNumber { get; set; }

    }
}
