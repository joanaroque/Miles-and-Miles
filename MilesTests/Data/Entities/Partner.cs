using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Entities
{
    public class Partner: IEntity
    {
        public int Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User ModifiedBy { get; set; }
        public bool IsConfirm { get; set; }
        public int Status { get; set; }

        /************OBJECT PROPERTIES****************************/

        public string CompanyName { get; set; }


        public string Address { get; set; }


        public string Description { get; set; }


        public string Designation { get; set; }

        public string Url { get; set; }


        public byte Logo { get; set; }


        public int MyProperty { get; set; }
    }
}
