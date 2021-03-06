﻿namespace MilesBackOffice.Web.Models
{
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartnerViewModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }


        [Required]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Company's URL")]
        public string Url { get; set; }


        /// <summary>
        /// Area of Work
        /// can be a set number of areas of work
        /// </summary>
        /// TODO maybe transform into a class with areas of work ex:
        /// hotels, souvenir shops, transport companies
        [Required]
        public PartnerType Designation { get; set; }


        public IEnumerable<SelectListItem> PartnerTypes { get; set; }



        public IFormFile Logo { get; set; }


        [Required]
        public string Description { get; set; }


        public int Status { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime PartnerSince { get; set; }


        public string PartnerGuidId { get; set; }
    }

}
