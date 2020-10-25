namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification : IEntity
    {

        public User Client { get; set; }



        public bool IsRead { get; set; }


        //*************************************************************

        public NotificationType NotificationType { get; set; }



        [Required]
        public string Title { get; set; }



        [Required]
        public string Message { get; set; }



        public int Id { get; set; }



        public User CreatedBy { get; set; }




        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }



        public int Status { get; set; }


    }
}
