using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class ComplaintClientViewModel
    {

        public string ComplaintId { get; set; }



        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Title { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public DateTime Date { get; set; }



        public string Subject { get; set; }



        public string Reply { get; set; }



        public bool IsProcessed { get; set; }

    }
}
