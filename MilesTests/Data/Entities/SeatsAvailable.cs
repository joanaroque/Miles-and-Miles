namespace MilesBackOffice.Web.Data.Entities
{
    
    using System;


    public class SeatsAvailable : IEntity
    {

        public int FlightNumber { get; set; }



        public int MaximumSeats { get; set; }


        public int AvailableSeats { get; set; }




        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }



        public int Status { get; set; }
    }
}
