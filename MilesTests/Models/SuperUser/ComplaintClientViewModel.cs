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


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public DateTime Date { get; set; }



        public string Subject { get; set; }



        public string Reply { get; set; }



        public bool NotProcessed { get; set; }

    }
}
