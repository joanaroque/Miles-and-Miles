using Microsoft.AspNetCore.Identity;

using MilesTests.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MilesTests.Data.Entities
{
    public class User : IdentityUser
    {

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string LastName { get; set; }


        [MaxLength(20, ErrorMessage = "The filed {0} must contain less than {1} characteres.")]
        [Required]
        public string Document { get; set; }



        public string RoleId { get; set; }



        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }



        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime? DateOfBirth { get; set; }


        public string TIN { get; set; }


        public string Gender { get; set; }



        [Display(Name = "Image")]
        public Guid ImageId { get; set; }



        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public City City { get; set; }

    }
}
