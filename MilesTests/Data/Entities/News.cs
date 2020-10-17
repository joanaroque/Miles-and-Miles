namespace MilesBackOffice.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class News : IEntity
    {
        public string Title { get; set; }


        public string Body { get; set; }


        public List<byte> Images { get; set; }

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
