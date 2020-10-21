﻿namespace MilesBackOffice.Web.Models.SuperUser
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
        public string Body { get; set; }



        public string Reply { get; set; }



        public string ClientName { get; set; }




        public int Status { get; set; }


    }
}
