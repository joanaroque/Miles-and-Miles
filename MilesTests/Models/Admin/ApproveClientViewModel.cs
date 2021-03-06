﻿namespace MilesBackOffice.Web.Models.Admin
{
    using CinelAirMilesLibrary.Common.CustomValidation;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ApproveClientViewModel
    {
        public string Id { get; set; }


        [Display(Name = "Full Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }


        [EmailAddress]
        public string Email { get; set; }



        public string Username { get; set; }



        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        [RegularExpression(@"\d{9}",
         ErrorMessage = "Must insert the {0} correct.")]
        public string PhoneNumber { get; set; }



        public IEnumerable<SelectListItem> Roles { get; set; }



        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Must be at least 2 years old.")]
        public DateTime? DateOfBirth { get; set; }



        public int StatusMiles { get; set; }



        public int BonusMiles { get; set; }



        public TierType Status { get; set; }



        public Gender Gender { get; set; }




        [MaxLength(20)]
        [Required]
        public string TIN { get; set; }




        [Display(Name = "Country")]
        public int CountryId { get; set; }



        public string City { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }


        public IEnumerable<SelectListItem> StatusList { get; set; }


        public IEnumerable<SelectListItem> Genders { get; set; }



        public bool IsActive { get; set; }

        public bool IsApproved { get; set; }
    }
}
