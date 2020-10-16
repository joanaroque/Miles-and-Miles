using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Entities
{
    public class News : IEntity
    {
        public int Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User ModifiedBy { get; set; }
        public bool IsConfirm { get; set; }
        public int Status { get; set; }

        /************OBJECT PROPERTIES****************************/

        public string Title { get; set; }


        public string Body { get; set; }


        public List<byte> Images { get; set; }


    }
}
