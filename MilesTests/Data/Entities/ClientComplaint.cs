using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Data.Entities
{
    public class ClientComplaint : IEntity
    {

        [Required]
        public string Title { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public DateTime Date { get; set; }



        public string Subject { get; set; }



        public string Reply { get; set; }

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
