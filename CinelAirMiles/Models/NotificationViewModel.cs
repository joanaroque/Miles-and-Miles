namespace CinelAirMiles.Models
{
    using CinelAirMilesLibrary.Common.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;


    public class NotificationViewModel
    {

        public int NotiId { get; set; }



        public string Title { get; set; }



        public bool IsRead { get; set; }



        public string Message { get; set; }



        public NotificationType NotificationType { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }

}