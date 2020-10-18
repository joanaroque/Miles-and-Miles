using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using MilesBackOffice.Web.CustomValidation;
using MilesBackOffice.Web.Enums;

namespace MilesBackOffice.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "Full Name")]
        //[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }


        [EmailAddress]
        public string Email { get; set; }



        public string Username { get; set; }



        //[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
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
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime? DateOfBirth { get; set; }


        [MaxLength(20)]
        //[Required]
        public string Document { get; set; }


        [Display(Name = "Image")]
        public Guid ImageId { get; set; }



        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }



        //[Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }



        //[Required]
        public int CityId { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }



        public bool IsActive { get; set; }
    }
}
