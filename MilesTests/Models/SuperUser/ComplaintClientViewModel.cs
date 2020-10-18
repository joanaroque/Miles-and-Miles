using MilesBackOffice.Web.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models.SuperUser
{
    public class ComplaintClientViewModel : ClientComplaint
    {

        public int ComplaintId { get; set; }



        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }



        public bool IsProcessed { get; set; }

    }
}
