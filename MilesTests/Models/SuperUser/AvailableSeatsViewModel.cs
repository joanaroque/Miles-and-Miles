using MilesBackOffice.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class AvailableSeatsViewModel : SeatsAvailable
    {
        public string UserId { get; set; }


        [Required]
        public string Name { get; set; }

    }
}
