namespace MilesBackOffice.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using MilesBackOffice.Web.Enums;

    public class PremiumOfferViewModel
    {
        public int Id { get; set; }


        public string Title { get; set; }

        /// <summary>
        /// list of flights
        /// </summary>
        public IEnumerable<SelectListItem> Flights { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a flight from the list.")]
        public string FlightId { get; set; }



        public IEnumerable<SelectListItem> PartnersList { get; set; }



        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Partner from the list.")]
        public int PartnerId { get; set; }



        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Quantity { get; set; }



        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Price { get; set; }


        [Required]
        public string Conditions { get; set; }


        public PremiumType Type { get; set; }
    }
}
