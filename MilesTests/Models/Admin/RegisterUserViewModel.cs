using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.Rendering;

using MilesBackOffice.Web.CustomValidation;
using MilesBackOffice.Web.Enums;

namespace MilesBackOffice.Web.Models.Admin
{
    public class RegisterUserViewModel
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



        [Display(Name = "Name")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Name { get; set; }



        public string Document { get; set; }


        public string Gender { get; set; }


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



        [Display(Name = "Country")]
        public int CountryId { get; set; }



        [Display(Name = "City")]
        public int CityId { get; set; }




        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }



        //[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }




        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime? DateOfBirth { get; set; }



        public IEnumerable<SelectListItem> Roles { get; set; }



        public IEnumerable<SelectListItem> Cities { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }



        public string PhoneNumber { get; set; }



        public bool IsActive { get; set; }

    }
}
