using System;

namespace MilesBackOffice.Web.Data.Entities
{
    public class TierChange : IEntity
    {

        public string OldTier { get; set; }


        public string NewTier { get; set; }


        public int NumberOfFlights { get; set; }


        public long NumberOfMiles { get; set; }


        /************OBJECT PROPERTIES****************************/

        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }


        public bool IsConfirm { get; set; }


        public int Status { get; set; }
    }
}
