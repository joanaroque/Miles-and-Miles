using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class ComplaintClientViewModel
    {

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]

        public string Title { get; set; }


        [Required]
        public DateTime Date { get; set; }



        public string Subject { get; set; }



        public string Replay { get; set; }



        public bool NotProcessed { get; set; }

    }
}
