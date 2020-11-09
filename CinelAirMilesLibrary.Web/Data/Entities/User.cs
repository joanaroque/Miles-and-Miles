namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.CustomValidation;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {

        public string GuidId { get; set; }


        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }


        
        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }



        [MaxLength(150, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        [CustomBirthDateValidator(ErrorMessage = "Birth Date must be less than or equal to Today's day")]
        public DateTime? DateOfBirth { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        public string TIN { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        public Gender Gender { get; set; }


        public string City { get; set; }


        public Country Country { get; set; }


        public bool IsApproved { get; set; }


        public bool IsActive { get; set; }


        public int StatusMiles { get; set; }


        public int BonusMiles { get; set; }


        public TierType Tier { get; set; }
    }
}
