using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Data.Entities
{
    public class Advertising : IEntity
    {

        public string Title { get; set; }



        public string Content { get; set; }



        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

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
