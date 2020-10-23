﻿namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.CustomValidation;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {

        //[MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }


        //[MaxLength(20, ErrorMessage = "The filed {0} must contain less than {1} characteres.")]
        //[Required]
        public string Document { get; set; }


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


        public string TIN { get; set; }


        public string Gender { get; set; }



        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        public City City { get; set; }


        public Country Country { get; set; }


        public bool IsApproved { get; set; }


        public bool IsActive { get; set; }



        public int StatusMiles { get; set; }



        public int BonusMiles { get; set; }



        public TierType Status { get; set; }



    }
}