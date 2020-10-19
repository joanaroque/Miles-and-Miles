namespace MilesBackOffice.Web.Models.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class ComplaintClientViewModel 
    {

        public int ComplaintId { get; set; }


        [Required]
        public string Title { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Required]
        public string Subject { get; set; }



        public string Reply { get; set; }


        [Required]
        public User Client { get; set; }




        public bool IsProcessed { get; set; }

    }
}
