using MilesBackOffice.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models.SuperUser
{
    public class TierChangeViewModel : TierChange
    {
        public string UserId { get; set; }


        [Required]
        public string ClientName { get; set; }

    }
}
