namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Flight : IEntity
    {
        public string Departure { get; set; }



        public string Arrival { get; set; }



        public int MaximumSeats { get; set; }



        public int AvailableSeats { get; set; }



        public Partner Partner { get; set; }



        public int Miles { get; set; }



        public int Id { get; set; }



        public User CreatedBy { get; set; }



        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }



        public int Status { get; set; }
    }
}
