using MilesBackOffice.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class TierChangeViewModel : TierChange
    {
        public int TierChangeId { get; set; }


        [Required]
        public string ClientName { get; set; }

    }
}
