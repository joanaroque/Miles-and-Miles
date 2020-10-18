using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;

using System;

namespace MilesBackOffice.Web.Helpers
{
    public interface IConverterHelper
    {
        Advertising ToAdvertising(AdvertisingViewModel model, Guid imageId, bool isNew);


        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);


        ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew);


        ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);


        SeatsAvailable ToSeatsAvailable(AvailableSeatsViewModel model, bool isNew);


        AvailableSeatsViewModel ToAvailableSeatsViewModel(SeatsAvailable seatsAvailable);


        TierChange ToTierChange(TierChangeViewModel model, bool isNew);


        TierChangeViewModel ToTierChangeViewModel(TierChange tierChange);
    }
}
