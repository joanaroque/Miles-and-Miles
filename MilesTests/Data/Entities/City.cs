using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilesBackOffice.Web.Data.Entities
{
    public class City : IEntity
    {
        public int Id { get; set; }

        
        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }

        
        public User ModifiedBy { get; set; }


        public bool IsConfirm { get; set; }


        public int Status { get; set; }

        /*************OBJECT PROPERTIES*********************/

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }

    }
}
