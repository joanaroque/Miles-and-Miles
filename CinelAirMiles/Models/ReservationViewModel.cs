namespace CinelAirMiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class ReservationViewModel
    {

        public int ReservationId { get; set; }



        public string ClientName { get; set; }



        public string Destination { get; set; }



        public string PartnerName { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public int Status { get; set; }
    }
}
