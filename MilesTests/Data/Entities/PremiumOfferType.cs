namespace MilesBackOffice.Web.Data.Entities
{
    using System;
    using MilesBackOffice.Web.Enums;

    public class PremiumOfferType : IEntity
    {
        public PremiumType Type { get; set; }


        /************OBJECT PROPERTIES****************************/


        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }


        public int Status { get; set; }
    }
}
