namespace CinelAirMiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class NotificationViewModel
    {

        public int NotiId { get; set; }



        public string ClientName { get; set; }




        public string Message { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public int Status { get; set; }

    }

}