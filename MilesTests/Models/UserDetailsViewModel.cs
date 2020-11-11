namespace MilesBackOffice.Web.Models
{
    using CinelAirMilesLibrary.Common.CustomValidation;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        [DisplayName("Miles #ID")]
        public string GuidId { get; set; }


        [Display(Name = "Full Name")]
        public string Name { get; set; }


        [EmailAddress]
        public string Email { get; set; }


        public string Username { get; set; }


        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Role")]
        public UserType SelectedRole { get; set; }


        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }


        public int StatusMiles { get; set; }


        public int BonusMiles { get; set; }


        public TierType Status { get; set; }


        public Gender Gender { get; set; }


        [DisplayName("Tax Number")]
        public string TIN { get; set; }


        [Display(Name = "Country")]
        public int CountryId { get; set; }


        public bool IsActive { get; set; }


        public string City { get; set; }
    }
}
