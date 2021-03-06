﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinelAirMiles.Models
{
    public class ChangeUserViewModel
    {
        
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }



        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Address { get; set; }



        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string PhoneNumber { get; set; }


        [Required]
        public string TIN { get; set; }



        [Display(Name = "City")]
        public string City { get; set; }






        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        public int CountryId { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
