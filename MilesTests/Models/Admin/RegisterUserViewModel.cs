using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CinelAirMilesLibrary.Common.CustomValidation;
using CinelAirMilesLibrary.Common.Enums;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MilesBackOffice.Web.Models.Admin
{
    public class RegisterUserViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Display(Name = "Username")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Username { get; set; }



        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string TIN { get; set; }


        public Gender Gender { get; set; }


        [Display(Name = "Country")]
        public int CountryId { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }


        [Required]
        [MaxLength(150, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Must be at least 2 years old.")]
        public DateTime? DateOfBirth { get; set; }



        public IEnumerable<SelectListItem> Roles { get; set; }



        public IEnumerable<SelectListItem> Cities { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }



        public IEnumerable<SelectListItem> Genders { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
