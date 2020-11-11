namespace MilesBackOffice.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CinelAirMilesLibrary.Common.Enums;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PremiumOfferViewModel
    {
        public int Id { get; set; }


        public string Title { get; set; }

        /// <summary>
        /// list of flights
        /// </summary>
        public IEnumerable<SelectListItem> Flights { get; set; }


        
        //[Range(1, double.MaxValue, ErrorMessage = "Choose a flight from the list.")]
        [DisplayName("Flight")]
        public int FlightId { get; set; }



        public IEnumerable<SelectListItem> PartnersList { get; set; }



        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Partner from the list.")]
        [DisplayName("From Partner")]
        public int PartnerId { get; set; }


        public string PartnerName { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Quantity { get; set; }



        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Price { get; set; }


        [Required]
        public string Conditions { get; set; }


        public int AvailableSeats { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }


        public PremiumType Type { get; set; }


        public string OfferGuidId { get; set; }



        public string Arrival { get; set; }



        public string Departure { get; set; }


        public DateTime FlightDateTime { get; set; }
    }
}
