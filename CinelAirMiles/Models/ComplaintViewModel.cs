namespace CinelAirMiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ComplaintViewModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name ="Subject")]
        public string Title { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }



        public string Body { get; set; }

    }
}
