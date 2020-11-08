namespace MilesBackOffice.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdvertisingViewModel
    {
        public int Id { get; set; }


        public IEnumerable<SelectListItem> PartnersList { get; set; }



        public Partner Partner { get; set; }



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

        public string PostGuidId { get; set; }
    }
}
