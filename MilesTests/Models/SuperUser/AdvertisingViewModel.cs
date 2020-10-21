﻿namespace MilesBackOffice.Web.Models.SuperUser
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class AdvertisingViewModel
    {
        public int AdvertisingId { get; set; }



        public string PartnerId { get; set; }



        public IEnumerable<SelectListItem> Partners { get; set; }




        public string Title { get; set; }



        public string Content { get; set; }



        public int Status { get; set; }




        [Display(Name = "Image")]
        public Guid ImageId { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }



        public string Observations { get; set; }
    }
}
