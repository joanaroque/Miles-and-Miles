namespace MilesBackOffice.Web.Data.Entities
{
    using System;

    public class Partner : IEntity
    {
        public string CompanyName { get; set; }


        public string Address { get; set; }


        public string Description { get; set; }


        public string Designation { get; set; }


        public string Url { get; set; }


        public byte Logo { get; set; }



        /************OBJECT PROPERTIES****************************/


        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }


        public int Status { get; set; }
    }
}
