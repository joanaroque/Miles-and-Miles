namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;

    public class Reservation : IEntity
    {


        public string Cancellation { get; set; }


        public int Id { get; set; }



        public User CreatedBy { get; set; }



        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }


        public int Status { get; set; }
    }
}
