namespace MilesBackOffice.Web.Data.Entities
{
    using System;

    public class TypePremium : IEntity
    {
        public string Description { get; set; }


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
