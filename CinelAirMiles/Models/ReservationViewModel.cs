﻿namespace CinelAirMiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationViewModel
    {
        public int Id { get; set; }


        public string ReservationId { get; set; }


        public int PremiumOfferId { get; set; }

        public string Arrival { get; set; }



        public string Name { get; set; }



        public string Departure { get; set; }



        public string PartnerName { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }


        public int Status { get; set; }
    }
}
