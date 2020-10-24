namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification : IEntity
    {

        public int ExpiringMiles { get; set; }


        [Required]
        public string Title { get; set; }



        [Required]
        public string Message { get; set; }



        public int Id { get; set; }



        public User CreatedBy { get; set; }



        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }



        public int Status { get; set; }


    }
}
