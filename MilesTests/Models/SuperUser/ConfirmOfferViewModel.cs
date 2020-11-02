using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Enums;

namespace MilesBackOffice.Web.Models.SuperUser
{
    public class ConfirmOfferViewModel
    {
        public string OfferId { get; set; }

        public string AvailableSeats { get; set; }

        public Partner Partner { get; set; }

        public string Title { get; set; }

        public PremiumType Type { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
