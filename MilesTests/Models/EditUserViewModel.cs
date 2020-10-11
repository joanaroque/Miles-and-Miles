﻿using Microsoft.AspNetCore.Mvc.Rendering;
using MilesBackOffice.Web.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
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


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime? DateOfBirth { get; set; }


        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }



        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public int CityId { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }

    }
}
