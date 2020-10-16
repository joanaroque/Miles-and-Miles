using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using MilesBackOffice.Web.Data.Entities;

namespace MilesBackOffice.Web.Models
{
    public class PremiumIndexViewModel
    {
        public IEnumerable<PremiumOffer> PremiumOffers { get; set; }

        public CreateTicketViewModel TicketViewModel { get; set; }

        public CreateUpgradeViewModel UpgradeViewModel { get; set; }

        public CreateVoucherViewModel VoucherViewModel { get; set; }

    }

    public class CreateUpgradeViewModel
    {
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// list of flights
        /// </summary>
        public IEnumerable<string> Flights { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a flight from the list.")]
        public int FlightId { get; set; }


        
        public IEnumerable<Partner> PartnersList { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Partner from the list.")]
        public int PartnerId { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Quantity { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Price { get; set; }

    }

    public class CreateVoucherViewModel
    {
        [Required]
        public string Title { get; set; }


        public IEnumerable<Partner> PartnersList { get; set; }

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
    }

    public class CreateTicketViewModel
    {
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// list of flights
        /// </summary>
        public IEnumerable<string> Flights { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a flight from the list.")]
        public int FlightId { get; set; }



        public IEnumerable<Partner> PartnersList { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Choose a Partner from the list.")]
        public int PartnerId { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Quantity { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be a positive number!")]
        public int Price { get; set; }
    }
}
