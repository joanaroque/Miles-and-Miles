namespace CinelAirMiles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdvertisingViewModel
    {
        public int Id { get; set; }


        public IEnumerable<SelectListItem> PartnersList { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Partner from the list.")]
        public int PartnerId { get; set; }


        public string PartnerName { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a flight from the list.")]
        public int FlightId { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }


        public int Status { get; set; }


        [Display(Name = "Image Location")]
        public byte[] Image { get; set; }



        [Display(Name = "Image")]
        public List<IFormFile> ImageFile { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        public string PostGuidId { get; set; }


        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

    }
}