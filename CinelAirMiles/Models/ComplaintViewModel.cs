namespace CinelAirMiles.Models
{
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ComplaintViewModel
    {
        public int Id { get; set; }


        public IEnumerable<SelectListItem> Complaints { get; set; }


        [Required]
        [Display(Name = "Subject")]
        public ComplaintType Complaint { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }



        public string Body { get; set; }




        public int Status { get; set; }
    }
}
