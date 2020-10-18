using MilesBackOffice.Web.Data.Entities;

using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models.SuperUser
{
    public class ComplaintClientViewModel : ClientComplaint
    {

        public int ComplaintId { get; set; }


        [Required]
        public string ClientName { get; set; }



        public bool IsProcessed { get; set; }

    }
}
